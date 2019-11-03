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
using Android.Support.Design.Widget;
using System;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using System.Threading.Tasks;

namespace AndroMooda3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public const int REQUEST_CAMERA = 100;
        public LinearLayout rootView;
        private Camera2 camera;
        private TextureView cameraTextureView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            rootView = FindViewById<LinearLayout>(Resource.Id.rootView);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted)
            {
                this.RequestPermissions(new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
            }

            cameraTextureView = FindViewById<TextureView>(Resource.Id.cameraTextureView);
            camera = new Camera2(this, cameraTextureView, rootView);
            Log.Verbose("bib", camera.GetFrontCameraId());

            camera.StartPreview(cameraTextureView);
        }

        internal CameraManager GetCameraManager(string cameraService)
        {
            return GetSystemService(cameraService) as CameraManager;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == REQUEST_CAMERA)
            {
                if (grantResults[0].Equals(Permission.Denied)) 
                {
                    System.Environment.Exit(0);
                }
            }
        }
    }    
}