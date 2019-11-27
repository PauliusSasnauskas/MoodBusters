
using Android.Hardware.Camera2;
using Android.Support.Design.Widget;
using System;

namespace AndroMooda3.Callbacks
{
    public class CameraCaptureSessionCallback : CameraCaptureSession.StateCallback
    {
        private readonly CameraActivity activity;
        private readonly Camera2Impl camera;

        public CameraCaptureSessionCallback(CameraActivity activity, Camera2Impl camera)
        {
            this.activity = activity;
            this.camera = camera;
        }

        public override void OnConfigured(CameraCaptureSession session)
        {
            if (camera == null)
            {
                throw new NullReferenceException("Camera is null");
            }
            camera.session = session;
            camera.captureRequestBuilder.Set(CaptureRequest.ControlMode, 1);
            session.SetRepeatingRequest(camera.captureRequestBuilder.Build(), null, null);
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            throw new Exception("Camera configuration failed");
        }
    }
}