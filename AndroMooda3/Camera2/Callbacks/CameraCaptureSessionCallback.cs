
using Android.Hardware.Camera2;
using Android.Support.Design.Widget;

namespace AndroMooda3.Callbacks
{
    public class CameraCaptureSessionCallback : CameraCaptureSession.StateCallback
    {
        private readonly MainActivity activity;
        private readonly Camera2 camera;

        public CameraCaptureSessionCallback(MainActivity activity, Camera2 camera)
        {
            this.activity = activity;
            this.camera = camera;
        }

        public override void OnConfigured(CameraCaptureSession session)
        {
            camera.captureRequestBuilder.Set(CaptureRequest.ControlMode, 1);
            session.SetRepeatingRequest(camera.captureRequestBuilder.Build(), null, null);
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            Snackbar.Make(activity.rootView, "OnConfigureFailed", Snackbar.LengthShort).Show();
        }
    }
}