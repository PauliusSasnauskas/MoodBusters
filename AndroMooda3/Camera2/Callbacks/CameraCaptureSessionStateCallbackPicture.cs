
using Android.Hardware.Camera2;
using Android.Support.Design.Widget;
using System;

namespace AndroMooda3.Callbacks
{
    public class CameraCaptureSessionCallbackPicture : CameraCaptureSession.StateCallback
    {
        private readonly CameraActivity activity;
        private readonly Camera2Impl camera;

        public delegate void ConfiguredDelegate(CameraCaptureSession session);
        public event ConfiguredDelegate OnConfiguredEvent;

        public CameraCaptureSessionCallbackPicture(CameraActivity activity, Camera2Impl camera)
        {
            this.activity = activity;
            this.camera = camera;
        }

        public override void OnConfigured(CameraCaptureSession session)
        {
            try
            {
                OnConfiguredEvent(session);
            }
            catch (CameraAccessException e)
            {
                throw new Exception("Camera access exception.", e);
            }
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            throw new Exception("Camera capture session configuration failed");
        }
    }
}