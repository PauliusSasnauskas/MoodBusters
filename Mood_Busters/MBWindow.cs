using System;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Mood_Busters
{
    public partial class MBWindow : Form
    {
        private IRecognitionApi apiClient;
        private IErrorHandler apiErrorHandler;
        public MBWindow()
        {
            InitializeComponent();
            apiClient = new AmazonRekognitionApi();
			apiErrorHandler = new ErrorHandlerWindows();
        }

        private void updateFromImage(MemoryStream stream)
        {
            List<Mood> moods = apiClient.GetMoods(stream);
            if (moods == null)
            {
                moodLabel.Text = "Error";
                return;
            }
            moodLabel.Text = "";
            BoundingBoxPainter painter = new BoundingBoxPainterWindows(stream);
            painter.PaintAll(moods);
            analyzedImageBox.Image = painter.Image;

            //moods.ForEach(mood => moodLabel.Text += mood.ToString() + '\n');
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = StringConst.Filter + " | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    streaming_off = true;
                    getMoodButton.Text = StringConst.Resume;
                    string imageLocation = dialog.FileName;
                    //analyzedImageBox.ImageLocation = imageLocation;
                    updateFromImage(imageLocation.ToStream());
                }
            }
            catch (Exception)
            {
                apiErrorHandler.ShowError(StringConst.ErrBadImage, StringConst.ErrProccesing);
            }
        }

        Capture capture;

        private void MBWindow_Load(object sender, EventArgs e)
        {
            capture = new Capture();            
            Application.Idle += Streaming;
        }

        bool streaming_off = false;

        private void GetMoodButtonClick(Object sender, EventArgs e)
        {
            if (getMoodButton.Text == StringConst.Mood)
            {
                getMoodButton.Text = StringConst.Resume;
                MemoryStream memStream = new MemoryStream();
                analyzedImageBox.Image.Save(memStream, ImageFormat.Jpeg);
                streaming_off = true;
                updateFromImage(memStream);
            }
            else
            {
                streaming_off = false;
                getMoodButton.Text = StringConst.Mood;
            }
        }

        private void Streaming(object sender, EventArgs e)
        {
            if (streaming_off) return;
            var img = capture.QueryFrame().ToImage<Bgr, byte>();
            analyzedImageBox.Image = img.Bitmap;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif";
            saveFileDialog.FileName = StringConst.Capture;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat saveFormat;
                switch(saveFileDialog.FilterIndex) 
                {
                    case 1 :
                        saveFormat = ImageFormat.Jpeg;
                        break;
                    case 2 :
                        saveFormat = ImageFormat.Bmp;
                        break;
                    case 3 :
                        saveFormat = ImageFormat.Gif;
                        break;
                    default:
                        goto case 1;
                }
                analyzedImageBox.Image.Save(saveFileDialog.FileName, saveFormat);
            }
        }
    }
}
