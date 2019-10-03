using System;
using System.IO;

namespace Mood_Busters
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
            catch (Exception)       //TODO: Implement this normally with Error Handling class
            {
                Console.WriteLine(StringConst.ErrLoading + imageLocation);
                return null;
                //new Mood { Name = MoodName.Error, Confidence = 0 };     -THIS WAS THE CODE BEFORE MOVING THIS TO SEPARATE CLASS. MAY BE USEFUL         
            }
        }
    }
}
