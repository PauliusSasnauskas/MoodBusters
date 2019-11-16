using Android.Media;
using Android.Util;
using Java.Nio;
using System;
using System.IO;

namespace AndroMooda3.Listeners
{
    class CameraImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
    {
        private static readonly string LOG_TAG = "Camera2/CameraImageAvailableListener";

        public void OnImageAvailable(ImageReader reader)
        {
            Image image = null;
            try
            {
                image = reader.AcquireLatestImage();
                ByteBuffer buffer = image.GetPlanes()[0].Buffer;
                byte[] bytes = new byte[buffer.Capacity()];
                buffer.Get(bytes);
                save(bytes);
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
        private void save(byte[] bytes)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "pic.jpg");

            using (var thing = File.Create(filename))
            {
                thing.Write(bytes, 0, bytes.Length);
                thing.Flush();
                thing.Close();
            }
        }
    }
}