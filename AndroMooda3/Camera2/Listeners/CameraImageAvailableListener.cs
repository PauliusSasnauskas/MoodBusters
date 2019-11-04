using Android.Media;
using Android.Util;
using Java.IO;
using Java.Nio;

namespace AndroMooda3.Listeners
{
    class CameraImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
    {
        private static readonly string LOG_TAG = "Camera2/CameraImageAvailableListener";
        private File file;

        public CameraImageAvailableListener(File file)
        {
            this.file = file;
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
                save(bytes);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Log.Verbose(LOG_TAG, "File not found, " + e.Message);
            }
            catch (System.IO.IOException e)
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
            OutputStream output = null;
			try {
                output = new FileOutputStream(file);
                output.Write(bytes);
            } finally {
                if (null != output)
                {
                    output.Close();
                }
            }
        }
    }
}