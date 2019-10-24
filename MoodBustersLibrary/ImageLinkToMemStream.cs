using System;
using System.IO;

namespace MoodBustersLibrary
{
    public static class ImageLinkToMemStream
    {
        public static Func<string, MemoryStream> ToStream = imageLocation =>
        {
            try
            {
                using (FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    return new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                return null;
            }
        };
    }
}
