using Android;
using Android.Content;
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
        private readonly TextureView textureView;
        private readonly RelativeLayout rootView;
        public CaptureRequest.Builder captureRequestBuilder;
        private Surface surface;
        private CameraManager cameraManager;
        CameraCharacteristics cameraCharacteristics;
        private string camId = string.Empty;

        public Camera2(MainActivity activity, TextureView textureView, RelativeLayout rootView)
        {
            this.activity = activity;
            this.textureView = textureView;
            this.rootView = rootView;
        }

        public void Start()
        {
            textureView.SurfaceTextureListener = new Camera2SurfaceTextureListener(activity, this);
        }

        public void OpenCamera()
        {
            cameraManager.OpenCamera(camId, new CameraDeviceCallback(activity, this, surface), null);
        }


        public void TryCamera()
        {
            cameraManager = activity.GetCameraManager(Context.CameraService);

            //string[] cameraList = ;

            foreach (string s in cameraManager.GetCameraIdList())
            {
                cameraCharacteristics = cameraManager.GetCameraCharacteristics(s);
                camId = s;
                if (cameraCharacteristics.Get(CameraCharacteristics.LensFacing).Equals(LensFacing.Front))   //probably doesn't work
                {
                    break;
                }
            }

            if (camId == null || camId == "") return;

            //StreamConfigurationMap map = camChar.Get(CameraCharacteristics.ScalerStreamConfigurationMap) as StreamConfigurationMap;
            //Size size = map.GetOutputSizes(256)[0];

            if (!textureView.IsAvailable)
            {
                Snackbar.Make(rootView, "Surface view not available", Snackbar.LengthShort).Show();
                return; 
            }

            surface = new Surface(textureView.SurfaceTexture);


            if (ContextCompat.CheckSelfPermission(activity, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                activity.RequestPermissions(new string[] { Manifest.Permission.Camera }, MainActivity.REQUEST_CAMERA);
                return;
            }

            OpenCamera();
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool b)
        {
            if (b) 
            {
                surface.Dispose();
                cameraManager.Dispose();
                cameraCharacteristics.Dispose();
                return;
            }
        }
    }
}