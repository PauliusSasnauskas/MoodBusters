using System;
using System.IO;
using System.Windows.Forms;

namespace Mood_Busters
{
    static class ImageUploadDialog
    {
        /// <summary>
        /// Returns Memory Stream of image uploaded specified in upload dialog.
        /// </summary>
        /// <returns></returns>
        public static MemoryStream PictureStream()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = StringConst.Filter + " | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string imageLocation = dialog.FileName;
                    return imageLocation.ToStream();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                MBWindow.errorHandler.ShowError(StringConst.ErrBadImage, StringConst.ErrProccesing);
                return null;
            }
        }
    }
}
