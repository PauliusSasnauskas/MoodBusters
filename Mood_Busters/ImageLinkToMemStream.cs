using System;
using System.IO;

namespace AndroMooda3
{
    static class ImageLinkToMemStream
    {
        public static MemoryStream ToStream(this string imageLocation)
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
                MBWindow.errorHandler.ShowError(StringConst.ErrLoading + imageLocation);
                return null;      
            }
        }
    }
}
