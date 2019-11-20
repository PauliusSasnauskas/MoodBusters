using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Hardware.Camera2;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using System;
using System.IO;
using System.Linq;
using MoodBustersLibrary;
using System.Collections.Generic;
using Android.Support.Design.Widget;
using Android.Util;
using Javax.Net.Ssl;
using Java.Security;

namespace AndroMooda3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public const int REQUEST_CAMERA = 100;
        public View rootView;
        private Camera2Impl camera;
        private TextureView cameraTextureView;
        private ImageButton mCaptureButton;
        private ImageButton mCloseButton;
        private Button errorButton;
        private ErrorHandlerAndroid errorHandler;

        private readonly static string LOG_TAG = "MainActivity";

        IRecognitionApi recognitionApi = new WebRequestRecognitionApi("https://192.168.164.73:45455/api/mood");

        private void trustEveryone()
        {
            try
            {
                HttpsURLConnection.DefaultHostnameVerifier = new VerifierSomething();
                SSLContext context = SSLContext.GetInstance("TLS");
                ITrustManager[] thing2 = new ITrustManager[]
                {
                    new TrustSomething()
                };
                context.Init(null, thing2, new SecureRandom());
                HttpsURLConnection.DefaultSSLSocketFactory = context.SocketFactory;
            } catch (Exception e) { // should never happen 
                Log.Verbose("asd", e.StackTrace);
            } 
        } 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            errorHandler = new ErrorHandlerAndroid(this);

            rootView = FindViewById<RelativeLayout>(Resource.Id.rootView);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
            }

            cameraTextureView = FindViewById<TextureView>(Resource.Id.cameraTextureView);
            camera = new Camera2Impl(this, cameraTextureView, rootView, () =>
            {
                mCaptureButton.Visibility = ViewStates.Gone;
                mCloseButton.Visibility = ViewStates.Visible;
            }, () =>
            {
                mCloseButton.Visibility = ViewStates.Gone;
                mCaptureButton.Visibility = ViewStates.Visible;
            }, async (byte[] bytes) =>
            {
                IEnumerable<Mood> moods;
                MemoryStream ms = new MemoryStream(bytes);
                try
                {
                    trustEveryone();
                    moods = await recognitionApi.GetMoodsAsync(ms);
                }
                catch (AggregateException e)
                {
                    Snackbar.Make(rootView, "Failed to process your mood.", Snackbar.LengthShort).Show();
                    Log.Verbose(LOG_TAG, e.Message);
                    Log.Verbose(LOG_TAG, e.StackTrace);
                    return;
                }
                MoodName bestMood = moods.Where((x) => x.Confidence == moods.Max((y) => y.Confidence)).Select((z) => z.Name).First();
                Snackbar.Make(rootView, $"You feel {bestMood.ToString()}", Snackbar.LengthShort).Show();
            });
            camera.StartPreview(cameraTextureView);

            mCaptureButton = FindViewById<ImageButton>(Resource.Id.button_capture);
            mCloseButton = FindViewById<ImageButton>(Resource.Id.button_close);
            errorButton = FindViewById<Button>(Resource.Id.error_button);

            errorButton.Click += delegate { errorHandler.ShowError("Error Handler Works!!"); };

            mCaptureButton.Click += (e, v) => camera.TakePicture();
            mCloseButton.Click += (e, v) => camera.ResumePreview();

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
                if (grantResults[0].Equals(Android.Content.PM.Permission.Denied)) 
                {
                    System.Environment.Exit(0);
                }
            }
        }
    }    
}