
using Android.Hardware.Camera2;
using Android.Support.Design.Widget;

namespace AndroMooda3.Callbacks
{
    public class CameraCaptureSessionCallbackPicture : CameraCaptureSession.StateCallback
    {
        private readonly MainActivity activity;
        private readonly Camera2Impl camera;
        private readonly CaptureRequest.Builder builder;
        private readonly CameraCaptureSession.CaptureCallback callback;

        public delegate void ConfiguredDelegate(CameraCaptureSession session);
        public event ConfiguredDelegate OnConfiguredEvent;

        public CameraCaptureSessionCallbackPicture(MainActivity activity, Camera2Impl camera)
        {
            this.activity = activity;
            this.camera = camera;
        }

        public override void OnConfigured(CameraCaptureSession session)
        {
            try
            {
                OnConfiguredEvent(session);
                //session.Capture(builder.Build(), callback, null);
            }
            catch (CameraAccessException e)
            {
                Snackbar.Make(activity.rootView, "CameraAccessException: " + e.Message, Snackbar.LengthShort).Show();
            }
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            Snackbar.Make(activity.rootView, "OnConfigureFailed", Snackbar.LengthShort).Show();
        }
    }
}