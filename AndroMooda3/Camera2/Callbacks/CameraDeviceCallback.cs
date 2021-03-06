﻿using System;
using System.Collections.Generic;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Runtime;
using Android.Views;

namespace AndroMooda3.Callbacks
{
    public class CameraDeviceCallback : CameraDevice.StateCallback
    {
        private readonly Surface surface;
        private readonly CameraActivity activity;
        private readonly Camera2Impl camera;

        public CameraDeviceCallback(CameraActivity activity, Camera2Impl camera, Surface surface)
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
            throw new Exception("CameraDevice state error: " + error.ToString());
        }

        public override void OnOpened(CameraDevice camera)
        {
            if (camera == null)
            {
                throw new Exception("No camera device found." );
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
