using Android;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace AndroMooda3
{
    internal class CameraImplementation
    {
        private MainActivity activity;
        private TextureView textureView;
        private RelativeLayout rootView;
        public CaptureRequest.Builder crb;

        public CameraImplementation(MainActivity activity, TextureView textureView, RelativeLayout rootView)
        {
            this.activity = activity;
            this.textureView = textureView;
            this.rootView = rootView;
        }

        public void Start()
        {
            textureView.SurfaceTextureListener = new MySurfaceTextureListener(activity, this);
        }

        Surface surface;
        string camId;
        CameraManager cameraManager;

        public void tryCamera()
        {
            cameraManager = activity.GetCameraManager(Context.CameraService);

            string[] cameraList = cameraManager.GetCameraIdList();

            camId = cameraList[0];
            CameraCharacteristics camChar = cameraManager.GetCameraCharacteristics(camId);

            foreach (string s in cameraList)
            {
                camChar = cameraManager.GetCameraCharacteristics(s);
                camId = s;
                if (camChar.Get(CameraCharacteristics.LensFacing).Equals(LensFacing.Front))
                {
                    break;
                }
            }

            if (camId == null || camId.Equals("")) return;

            StreamConfigurationMap map = camChar.Get(CameraCharacteristics.ScalerStreamConfigurationMap) as StreamConfigurationMap;
            Size size = map.GetOutputSizes(256)[0];

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

            openCamera();
        }

        public void openCamera()
        {
            cameraManager.OpenCamera(camId, new MyStateListener(activity, this, surface), null);
        }


        public class MyStateCallback : CameraCaptureSession.StateCallback
        {
            private MainActivity activity;
            private CameraImplementation ci;

            public MyStateCallback(MainActivity activity, CameraImplementation ci)
            {
                this.activity = activity;
                this.ci = ci;
            }
            public override void OnConfigured(CameraCaptureSession session)
            {
                ci.crb.Set(CaptureRequest.ControlMode, 1);
                session.SetRepeatingRequest(ci.crb.Build(), null, null);
            }

            public override void OnConfigureFailed(CameraCaptureSession session)
            {
                Snackbar.Make(activity.rootView, "OnConfigureFailed", Snackbar.LengthShort).Show();
            }
        }
        public class MyStateListener : CameraDevice.StateCallback
        {
            private Surface sf;
            private MainActivity activity;
            CameraImplementation ci;

            public MyStateListener(MainActivity activity, CameraImplementation ci, Surface sf)
            {
                this.ci = ci;
                this.sf = sf;
                this.activity = activity;
            }

            public override void OnDisconnected(CameraDevice camera)
            {
                camera.Close();
            }

            public override void OnError(CameraDevice camera, [GeneratedEnum] CameraError error)
            {
                Snackbar.Make(activity.rootView, "CameraDevice State Error", Snackbar.LengthShort).Show();
            }

            public override void OnOpened(CameraDevice camera)
            {
                List<OutputConfiguration> loc = new List<OutputConfiguration>();
                loc.Add(new OutputConfiguration(sf));
                SessionConfiguration cfg = new SessionConfiguration(0, loc, AsyncTask.ThreadPoolExecutor, new MyStateCallback(activity, ci));
                camera.CreateCaptureSession(cfg);
                ci.crb = camera.CreateCaptureRequest(CameraTemplate.Preview);
                ci.crb.AddTarget(sf);
            }
        }

        class MySurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
        {
            private CameraImplementation ci;
            private MainActivity mainActivity;
            public MySurfaceTextureListener(MainActivity mainActivity, CameraImplementation ci)
            {
                this.ci = ci;
                this.mainActivity = mainActivity;
            }
            public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
            {
                ci.tryCamera();
            }
            public bool OnSurfaceTextureDestroyed(SurfaceTexture surface) => false;
            public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
            {
                Snackbar.Make(mainActivity.rootView, "OnSurfaceTextureSizeChanged", Snackbar.LengthShort).Show();
            }
            public void OnSurfaceTextureUpdated(SurfaceTexture surface)
            {
                //updated
            }
        }
    }
}