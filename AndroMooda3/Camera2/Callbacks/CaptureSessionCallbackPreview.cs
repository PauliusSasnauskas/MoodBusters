using Android.Hardware.Camera2;
using Android.Support.Design.Widget;
using Android.Widget;

namespace AndroMooda3
{
    internal class CaptureSessionCallbackPreview : CameraCaptureSession.StateCallback
    {
        private Camera2Impl c;
        private MainActivity a;
        public CaptureSessionCallbackPreview(Camera2Impl c, MainActivity a)
        {
            this.c = c;
            this.a = a;
        }
        public override void OnConfigured(CameraCaptureSession cameraCaptureSession)
        {
            //The camera is already closed
            if (null == c.cameraDevice)
            {
                return;
            }
            // When the session is ready, we start displaying the preview.
            c.session = cameraCaptureSession;
            c.ContinuePreview();
        }
        public override void OnConfigureFailed(CameraCaptureSession cameraCaptureSession)
        {
            Snackbar.Make(a.rootView, "Configuration change", Snackbar.LengthShort).Show();
        }
    }
}