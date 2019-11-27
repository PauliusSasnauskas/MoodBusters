using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using AndroMooda3.Callbacks;
using AndroMooda3.Listeners;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoodBustersLibrary;
using Orientation = Android.Media.Orientation;
using Autofac;

namespace AndroMooda3
{
    public class Camera2Impl : IDisposable
    {
        private readonly CameraActivity activity;
        private readonly TextureView textureView;
        private readonly View rootView;
        private readonly PreviewCallback showPreviewInterface;
        private readonly PreviewCallback hidePreviewInterface;
        private readonly PictureCallback pictureTaken;
        public CaptureRequest.Builder captureRequestBuilder;
        private CameraManager cameraManager;
        private CameraCharacteristics cameraCharacteristics;
        private string _FrontCameraId = null;
        internal CameraDevice cameraDevice;
        internal CameraCaptureSession session;
        private IErrorHandler errorHandler;

        //private readonly string LOG_TAG = "Camera2Impl";

        private int mWidth;
        private int mHeight;

        public string FrontCameraId {
            get {
                if (_FrontCameraId == null)
                {
                    _FrontCameraId = GetFrontCameraId();
                }
                return _FrontCameraId;
            }
            private set {
                _FrontCameraId = value;
            }
        }

        public delegate void PreviewCallback();
        public delegate void PictureCallback(byte[] bytes);

        internal void ShowImagePreview()
        {
            // show something to user
            // and call ResumePreview
            showPreviewInterface();
        }

        public void ResumePreview()
        {
            hidePreviewInterface();
            try
            {
                SurfaceTexture texture = textureView.SurfaceTexture;
                texture.SetDefaultBufferSize(mWidth, mHeight);
                Surface surface = new Surface(texture);
                captureRequestBuilder = cameraDevice.CreateCaptureRequest(CameraTemplate.Preview);
                captureRequestBuilder.AddTarget(surface);
                List<Surface> surfaces = new List<Surface>
                {
                    surface
                };
                cameraDevice.CreateCaptureSession(surfaces, new CaptureSessionCallbackPreview(this, activity), null);
            }
            catch (CameraAccessException e)
            {
                e.PrintStackTrace();
            }
        }

        internal void ContinuePreview()
        {
            if (null == cameraDevice)
            {
                errorHandler.ShowError("App error: cannot show preview.");
            }
            captureRequestBuilder.Set(CaptureRequest.ControlMode, (int)ControlMode.Auto);
            try
            {
                session.SetRepeatingRequest(captureRequestBuilder.Build(), null, null);
            }
            catch (CameraAccessException e)
            {
                e.PrintStackTrace();
            }
        }

        public Camera2Impl(IErrorHandler errorHandler, CameraActivity activity, TextureView textureView, View rootView, PreviewCallback showPreviewInterface, PreviewCallback hidePreviewInterface, PictureCallback pictureTaken)
        {
            this.errorHandler = errorHandler;
            this.activity = activity;
            this.textureView = textureView;
            this.rootView = rootView;
            this.showPreviewInterface = showPreviewInterface;
            this.hidePreviewInterface = hidePreviewInterface;
            this.pictureTaken = pictureTaken;
        }

        private string GetFrontCameraId()
        {
            cameraManager = activity.GetCameraManager(Context.CameraService);

            foreach (string s in cameraManager.GetCameraIdList())
            {
                cameraCharacteristics = cameraManager.GetCameraCharacteristics(s);        //obtained camera characteristics
                if (cameraCharacteristics.Get(CameraCharacteristics.LensFacing).Equals((int)LensFacing.Front))          //get front facing camera
                {
                    return s;
                }
            }
            errorHandler.ShowError("Could not find front facing camera");
            return null;

            // TODO: Create Exception "Front camera not found"
        }

        public void TakePicture()
        {
            if (cameraDevice == null)
            {
                errorHandler.ShowError("Cannot take picture; no camera detected");
                // TODO: Throw exception
            }
            CameraCharacteristics characteristics = cameraManager.GetCameraCharacteristics(FrontCameraId);
            Size[] jpegSizes = null;
            if (characteristics != null)
            {
                jpegSizes = (characteristics.Get(CameraCharacteristics.ScalerStreamConfigurationMap) as StreamConfigurationMap).GetOutputSizes((int)Android.Graphics.ImageFormatType.Jpeg);
            }
            int width = 640;
            int height = 480;
            if (jpegSizes != null && 0 < jpegSizes.Length)
            {
                width = jpegSizes[0].Width;
                height = jpegSizes[0].Height;
                mWidth = width;
                mHeight = height;
            }
            ImageReader reader = ImageReader.NewInstance(width, height, Android.Graphics.ImageFormatType.Jpeg, 1);
            List<Surface> outputSurfaces = new List<Surface>(2)
            {
                reader.Surface,
                new Surface(textureView.SurfaceTexture)
            };
            CaptureRequest.Builder captureBuilder = cameraDevice.CreateCaptureRequest(CameraTemplate.StillCapture);
            captureBuilder.AddTarget(reader.Surface);
            captureBuilder.Set(CaptureRequest.ControlMode, (int)ControlMode.Auto);

            SurfaceOrientation rotation = activity.WindowManager.DefaultDisplay.Rotation;
            Orientation orientation = 0;
            switch (rotation)
            {
                case SurfaceOrientation.Rotation0:
                    orientation = Orientation.Normal;
                    break;
                case SurfaceOrientation.Rotation90:
                    orientation = Orientation.Rotate90;
                    break;
                case SurfaceOrientation.Rotation180:
                    orientation = Orientation.Rotate180;
                    break;
                case SurfaceOrientation.Rotation270:
                    orientation = Orientation.Rotate270;
                    break;
            }
            captureBuilder.Set(CaptureRequest.JpegOrientation, (int)orientation);

            reader.SetOnImageAvailableListener(new CameraImageAvailableListener(pictureTaken), null);
            CameraCaptureSession.CaptureCallback captureListener = new CameraCaptureSessionCaptureCallback(rootView, this);

            var ccscc = new CameraCaptureSessionCallbackPicture(activity, this as Camera2Impl);
            ccscc.OnConfiguredEvent += (session) =>
            {
                session.Capture(captureBuilder.Build(), captureListener, null);
            };

            cameraDevice.CreateCaptureSession(outputSurfaces, ccscc, null);
        }

        public void StartPreview(TextureView textureView)
        {
            textureView.SurfaceTextureListener = new Camera2SurfaceTextureListener(activity, OpenCamera);
        }

        public void OpenCamera(Surface surface)
        {
            try
            {
                string camId = FrontCameraId;
                cameraManager.OpenCamera(camId, new CameraDeviceCallback(activity, this, surface), null);
            }
            catch (Exception)
            {
                errorHandler.ShowError("Front camera not available");
            }
        }

        //StreamConfigurationMap map = camChar.Get(CameraCharacteristics.ScalerStreamConfigurationMap) as StreamConfigurationMap;
        //Size size = map.GetOutputSizes(256)[0];

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool b)
        {
            if (b)
            {
                //surface.Dispose();
                cameraManager.Dispose();
                cameraCharacteristics.Dispose();
                return;
            }
        }
    }
}