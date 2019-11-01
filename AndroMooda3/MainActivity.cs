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

namespace AndroMooda3
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public const int REQUEST_CAMERA = 102;
        public RelativeLayout rootView;
        private Camera2 camera;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            rootView = FindViewById<RelativeLayout>(Resource.Id.rootView);

            TextureView cameraTextureView = FindViewById<TextureView>(Resource.Id.cameraTextureView);
            camera = new Camera2(this, cameraTextureView, rootView);
            camera.Start();
        }

        internal CameraManager GetCameraManager(string cameraService)
        {
            return GetSystemService(cameraService) as CameraManager;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == REQUEST_CAMERA)
            {
                camera.TryCamera();
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }    
}