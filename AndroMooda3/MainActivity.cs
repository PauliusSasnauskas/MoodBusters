using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Hardware.Camera2;
using System.Collections.Generic;
using Android.Util;
using Android.Hardware.Camera2.Params;
using Android.Hardware;
using Android.Graphics;

namespace AndroMooda3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public TextureView cameraTextureView;
        private CameraDevice.StateCallback stateListener;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            cameraTextureView = FindViewById<TextureView>(Resource.Id.cameraTextureView);
            CameraManager cameraManager = (CameraManager)GetSystemService(CameraService);

            string camId = "";

            new List<string>(cameraManager.GetCameraIdList()).ForEach((s) =>
            {
                if (cameraManager.GetCameraCharacteristics(s).Get(CameraCharacteristics.LensFacing).Equals(0))
                {
                    camId = s;
                }
            });

            if (camId.Equals("")) return;

            CameraCharacteristics cc = cameraManager.GetCameraCharacteristics(camId);
            StreamConfigurationMap map = cc.Get(CameraCharacteristics.ScalerStreamConfigurationMap) as StreamConfigurationMap;
            Size size = map.GetOutputSizes(256)[0];

            Surface aaa = new Surface(cameraTextureView.SurfaceTexture);
            stateListener = new MyStateListener(aaa);

            cameraManager.OpenCamera(camId, stateListener, null);



        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class MyStateListener : CameraDevice.StateCallback
        {
            Surface sf;
            public MyStateListener(Surface sf)
            {
                this.sf = sf;
            }

            public override void OnDisconnected(CameraDevice camera)
            {
                camera.Close();
            }

            public override void OnError(CameraDevice camera, [GeneratedEnum] Android.Hardware.Camera2.CameraError error)
            {
                Log.Verbose("bib", error.ToString());
            }

            public override void OnOpened(CameraDevice camera)
            {
                List<OutputConfiguration> loc = new List<OutputConfiguration>();
                loc.Add(new OutputConfiguration(sf));
                SessionConfiguration cfg = new SessionConfiguration(0, loc, AsyncTask.ThreadPoolExecutor, new MyStateCallback());
                camera.CreateCaptureSession(cfg);
            }
        }

        public class MyStateCallback : CameraCaptureSession.StateCallback
        {
            public override void OnConfigured(CameraCaptureSession session)
            {
                Log.Verbose("bib", session.ToString());
                //throw new System.NotImplementedException();
            }

            public override void OnConfigureFailed(CameraCaptureSession session)
            {
                Log.Verbose("bib", "error: " + session.ToString());
            }
        }
    }

    
}