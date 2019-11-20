using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AndroMooda3
{
    class SaveFileDialog : IImageSaver
    {
        public void Save(PictureBox pictureBox)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            System.Windows.Forms.SaveFileDialog aba = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif";
            saveFileDialog.FileName = StringConst.Capture;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat saveFormat;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        saveFormat = ImageFormat.Jpeg;
                        break;
                    case 2:
                        saveFormat = ImageFormat.Bmp;
                        break;
                    case 3:
                        saveFormat = ImageFormat.Gif;
                        break;
                    default:
                        goto case 1;
                }
                pictureBox.Image.Save(saveFileDialog.FileName, saveFormat);
            }
        }
    }
}
