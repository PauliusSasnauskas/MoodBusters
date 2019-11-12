using System.Collections.Generic;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;

namespace AndroMooda3.Callbacks
{
    public class CameraDeviceCallback : CameraDevice.StateCallback
    {
        private readonly Surface surface;
        private readonly MainActivity activity;
        private readonly Camera2Impl camera;

        public CameraDeviceCallback(MainActivity activity, Camera2Impl camera, Surface surface)
        {
            this.camera = camera;
            this.surface = surface;
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
            if (camera == null)
            {
                Snackbar.Make(activity.rootView, "No camera device found.", Snackbar.LengthShort).Show();
                return;
            }
            this.camera.cameraDevice = camera;
            List<OutputConfiguration> OutputConfigList = new List<OutputConfiguration>{ new OutputConfiguration(surface) };
            SessionConfiguration cfg = new SessionConfiguration(0, OutputConfigList, AsyncTask.ThreadPoolExecutor, new CameraCaptureSessionCallback(activity, this.camera));
            camera.CreateCaptureSession(cfg);
            this.camera.captureRequestBuilder = camera.CreateCaptureRequest(CameraTemplate.Preview);
            this.camera.captureRequestBuilder.AddTarget(surface);
        }
    }
    }
