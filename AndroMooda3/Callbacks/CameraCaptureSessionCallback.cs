
using Android.Hardware.Camera2;
using Android.Support.Design.Widget;

namespace AndroMooda3.Callbacks
{
    public class CameraCaptureSessionCallback : CameraCaptureSession.StateCallback
    {
        private MainActivity activity;
        private CameraImplementation ci;

        public CameraCaptureSessionCallback(MainActivity activity, CameraImplementation ci)
        {
            this.activity = activity;
            this.ci = ci;
        }

        public override void OnConfigured(CameraCaptureSession session)
        {
            ci.captureRequestBuilder.Set(CaptureRequest.ControlMode, 1);
            session.SetRepeatingRequest(ci.captureRequestBuilder.Build(), null, null);
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            Snackbar.Make(activity.rootView, "OnConfigureFailed", Snackbar.LengthShort).Show();
        }
    }
}