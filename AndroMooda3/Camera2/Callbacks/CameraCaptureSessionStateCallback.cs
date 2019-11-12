
using Android.Hardware.Camera2;
using Android.Support.Design.Widget;

namespace AndroMooda3.Callbacks
{
    public class CameraCaptureSessionCallback : CameraCaptureSession.StateCallback
    {
        private readonly MainActivity activity;
        private readonly Camera2Impl camera;

        public CameraCaptureSessionCallback(MainActivity activity, Camera2Impl camera)
        {
            this.activity = activity;
            this.camera = camera;
        }

        public override void OnConfigured(CameraCaptureSession session)
        {
            if (camera == null)
            {
                // TODO: fix something if camera null
                Snackbar.Make(activity.rootView, "Camera is null", Snackbar.LengthShort).Show();
            }
            camera.captureRequestBuilder.Set(CaptureRequest.ControlMode, 1);
            session.SetRepeatingRequest(camera.captureRequestBuilder.Build(), null, null);
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            Snackbar.Make(activity.rootView, "OnConfigureFailed", Snackbar.LengthShort).Show();
        }
    }
}