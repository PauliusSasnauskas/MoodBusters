using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Mood_Busters
{
    public partial class MBWindow : Form
    {
        private IRecognitionApi apiClient;
        private ICameraBox cameraBox;
        private IImageSaver saveDialog;
        public static IErrorHandler errorHandler = new ErrorHandlerWindows();
        private MemoryStream memStream;

        public MBWindow()
        {
            InitializeComponent();      
            apiClient = new AmazonRekognitionApi();
            saveDialog = new SaveFileDialog();
            cameraBox = new CameraBox(analyzedImageBox);
        }

        private void MBWindow_Load(object sender, EventArgs e)
        {
            Application.Idle += cameraBox.StreamFrames;
        }

        private void updateFromImage(MemoryStream stream)
        {
            List<Mood> moods = apiClient.GetMoods(stream);
            if (moods == null)
            {
                return;
            }
            BoundingBoxPainter painter = new BoundingBoxPainterWindows(stream);
            painter.PaintAll(moods);
            analyzedImageBox.Image = painter.Image;

            //moods.ForEach(mood => moodLabel.Text += mood.ToString() + '\n');
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if ((memStream = ImageUploadDialog.PictureStream()) != null)
            {
                isResultScreen = true;
                getMoodButton.BackgroundImage = Properties.Resources.resume;
                cameraBox.StopCamera();
                updateFromImage(memStream);
            }
            else return;
        }

        private bool isResultScreen = false;
        private void GetMoodButtonClick(Object sender, EventArgs e)
        {
            // TODO: there may be a better method of changing the pictures
            if (!isResultScreen)
            {
                isResultScreen = true;
                getMoodButton.BackgroundImage = Properties.Resources.resume;
                memStream = new MemoryStream();
                analyzedImageBox.Image.Save(memStream, ImageFormat.Jpeg);
                cameraBox.StopCamera();
                updateFromImage(memStream);
            }
            else
            {
                isResultScreen = false;
                cameraBox.ResumeCamera();
                getMoodButton.BackgroundImage = Properties.Resources.take_picture;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveDialog.Save(analyzedImageBox);
        }
    }
}
