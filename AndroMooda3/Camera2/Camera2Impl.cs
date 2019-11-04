using Android.Content;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using AndroMooda3.Callbacks;
using AndroMooda3.Listeners;
using Java.IO;
using System;
using System.Collections.Generic;
using Orientation = Android.Media.Orientation;

namespace AndroMooda3
{
    public class Camera2Impl : IDisposable
    {
        private readonly MainActivity activity;
        private readonly TextureView textureView;
        private readonly View rootView;
        public CaptureRequest.Builder captureRequestBuilder;
        //private Surface surface;
        private CameraManager cameraManager;
        private CameraCharacteristics cameraCharacteristics;
        private string _FrontCameraId = null;
        internal CameraDevice cameraDevice;

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


        public void ShowImagePreview()
        {
            // TODO: implement
        //    try
        //    {
        //        SurfaceTexture texture = textureView.getSurfaceTexture();
        //        assert texture != null;
        //        texture.setDefaultBufferSize(imageDimension.getWidth(), imageDimension.getHeight());
        //        Surface surface = new Surface(texture);
        //        captureRequestBuilder = cameraDevice.createCaptureRequest(CameraDevice.TEMPLATE_PREVIEW);
        //        captureRequestBuilder.addTarget(surface);
        //        cameraDevice.createCaptureSession(Arrays.asList(surface), new CameraCaptureSession.StateCallback(){
                //@Override
                //public void onConfigured(@NonNull CameraCaptureSession cameraCaptureSession)
                //        {
                //            //The camera is already closed
                //            if (null == cameraDevice)
                //            {
                //                return;
                //            }
                //            // When the session is ready, we start displaying the preview.
                //            cameraCaptureSessions = cameraCaptureSession;
                //            updatePreview();
                //        }
                //        @Override
                //                public void onConfigureFailed(@NonNull CameraCaptureSession cameraCaptureSession)
                //        {
                //            Toast.makeText(AndroidCameraApi.this, "Configuration change", Toast.LENGTH_SHORT).show();
                //        }
                //}, null);
                //} catch (CameraAccessException e) {
                //         e.printStackTrace();
                //}
        }

        public Camera2Impl(MainActivity activity, TextureView textureView, View rootView)
        {
            this.activity = activity;
            this.textureView = textureView;
            this.rootView = rootView;
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

            Snackbar.Make(rootView, "Could not find front facing camera", Snackbar.LengthLong).Show();
            return null;

            // TODO: Create Exception "Front camera not found"
        }

        public void TakePicture()
        {
            if (cameraDevice == null)
            {
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
            /* final */ File file = new File(Android.OS.Environment.ExternalStorageDirectory + "/pic.jpg");
            reader.SetOnImageAvailableListener(new CameraImageAvailableListener(file), null);
            CameraCaptureSession.CaptureCallback captureListener = new CameraCaptureSessionCaptureCallback(rootView, file, this);

            cameraDevice.CreateCaptureSession(outputSurfaces, new CameraCaptureSessionCallbackPicture(activity, this as Camera2Impl, captureBuilder, captureListener), null);
        }

        public void StartPreview(TextureView textureView)
        {
            textureView.SurfaceTextureListener = new Camera2SurfaceTextureListener(activity, this);
        }

        public void OpenCamera(Surface surface)
        {
            string camId;
            try
            {
                camId = FrontCameraId;
                cameraManager.OpenCamera(camId, new CameraDeviceCallback(activity, this, surface), null);
            }
            catch (Exception)
            {
                Snackbar.Make(rootView, "Front camera not available", Snackbar.LengthShort).Show();
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