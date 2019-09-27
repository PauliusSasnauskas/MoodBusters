using System;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;

namespace Mood_Busters
{
    public partial class MBWindow : Form
    {
        private IRecognitionApi apiClient;
        public MBWindow()
        {
            InitializeComponent();
            apiClient = new AmazonRekognitionApi();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    streaming_off = true;
                    getMoodButton.Text = "Resume Exercising Your Face";
                    string imageLocation = dialog.FileName;
                    analisedImageBox.ImageLocation = imageLocation;
                    moodLabel.Text = apiClient.GetMood(imageLocation.ToStream()).ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not process the image.", "Error_processing", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (getMoodButton.Text == "Get Mood")
            {
                getMoodButton.Text = "Resume Exercising Your Face";
                MemoryStream memStream = new MemoryStream();
                analisedImageBox.Image.Save(memStream, ImageFormat.Jpeg);
                moodLabel.Text = apiClient.GetMood(memStream).ToString();
                streaming_off = true;
            }
            else
            {
                streaming_off = false;
                getMoodButton.Text = "Get Mood";
            }
        }

        private void Streaming(Object sender, System.EventArgs e)
        {
            if (streaming_off) return;
            var img = capture.QueryFrame().ToImage<Bgr, byte>();
            var bmp = img.Bitmap;
            analisedImageBox.Image = bmp;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif";
            saveFileDialog.FileName = "capture1";
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
                analisedImageBox.Image.Save(saveFileDialog.FileName, saveFormat);
            }
        }
    }
}
