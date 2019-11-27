using Android.Hardware.Camera2;
using Android.Support.Design.Widget;
using Android.Views;


namespace AndroMooda3.Callbacks
{
    class CameraCaptureSessionCaptureCallback : CameraCaptureSession.CaptureCallback
    {
        private readonly View v;
        private readonly Camera2Impl camera;
        public CameraCaptureSessionCaptureCallback(View v, Camera2Impl camera)
        {
            this.v = v;
            this.camera = camera;
        }
        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
            base.OnCaptureCompleted(session, request, result);
            Snackbar.Make(v, "Saved.", Snackbar.LengthShort).Show();
            camera.ShowImagePreview();
        }
    }
}