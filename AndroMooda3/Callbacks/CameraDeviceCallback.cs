using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace AndroMooda3.Callbacks
{
    public class CameraDeviceCallback : CameraDevice.StateCallback
    {
        private Surface surface;
        private MainActivity activity;
        private CameraImplementation ci;

        public CameraDeviceCallback(MainActivity activity, CameraImplementation ci, Surface surface)
        {
            this.ci = ci;
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
            List<OutputConfiguration> loc = new List<OutputConfiguration>();
            loc.Add(new OutputConfiguration(surface));
            SessionConfiguration cfg = new SessionConfiguration(0, loc, AsyncTask.ThreadPoolExecutor, new CameraCaptureSessionCallback(activity, ci));
            camera.CreateCaptureSession(cfg);
            ci.captureRequestBuilder = camera.CreateCaptureRequest(CameraTemplate.Preview);
            ci.captureRequestBuilder.AddTarget(surface);
        }
    }
    }
