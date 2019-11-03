using Android;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroMooda3.Callbacks;
using AndroMooda3.Listeners;
using System;

namespace AndroMooda3
{
    public class Camera2 : IDisposable
    {
        private readonly MainActivity activity;
        //private readonly TextureView textureView;
        private readonly LinearLayout rootView;
        public CaptureRequest.Builder captureRequestBuilder;
        //private Surface surface;
        private CameraManager cameraManager;
        private CameraCharacteristics cameraCharacteristics;


        public Camera2(MainActivity activity, TextureView textureView, LinearLayout rootView)
        {
            this.activity = activity;
            //this.textureView = textureView;
            this.rootView = rootView;
        }

        public string GetFrontCameraId()
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

            //TODO: Create Exception "Front camera not found"
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
                camId = GetFrontCameraId();
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