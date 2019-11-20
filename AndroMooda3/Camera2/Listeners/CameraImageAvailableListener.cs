using Android.Media;
using Android.Util;
using Java.Nio;
using System;
using System.IO;
using static AndroMooda3.Camera2Impl;

namespace AndroMooda3.Listeners
{
    class CameraImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
    {
        private static readonly string LOG_TAG = "Camera2/CameraImageAvailableListener";
        private readonly PictureCallback pictureTaken;

        public CameraImageAvailableListener(PictureCallback pictureTaken)
        {
            this.pictureTaken = pictureTaken;
        }

        public void OnImageAvailable(ImageReader reader)
        {
            Image image = null;
            try
            {
                image = reader.AcquireLatestImage();
                ByteBuffer buffer = image.GetPlanes()[0].Buffer;
                byte[] bytes = new byte[buffer.Capacity()];
                buffer.Get(bytes);
                Save(bytes);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Log.Verbose(LOG_TAG, "File not found, " + e.Message);
            }
            catch (IOException e)
            {
                Log.Verbose(LOG_TAG, "IO Exception, " + e.Message);
            }
            finally
            {
                if (image != null)
                {
                    image.Close();
                }
            }
        }
        private void Save(byte[] bytes)
        {
            pictureTaken(bytes);
        }
    }
}