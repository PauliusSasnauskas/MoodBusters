using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using MoodBustersLibrary;
using System.Threading.Tasks;

namespace Mood_Busters
{
    public partial class MBWindow : Form
    {
        private IRecognitionApi apiClient;
        private ICameraBox cameraBox;
        private IImageSaver saveDialog;
        public static IErrorHandler errorHandler = new ErrorHandlerWindows();
        private MemoryStream memStream;
        
        public MBWindow(IRecognitionApi apiClient)
        {
            InitializeComponent();
            this.apiClient = apiClient;
            saveDialog = new SaveFileDialog();
            cameraBox = new CameraBox(analyzedImageBox);
        }

        private void MBWindow_Load(object sender, EventArgs e)
        {
            Application.Idle += cameraBox.StreamFrames;
        }

        private async Task updateFromImageAsync(MemoryStream stream)
        {
            List<Mood> moods = await apiClient.GetMoodsAsync(stream);
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
                updateFromImageAsync(memStream);
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
                updateFromImageAsync(memStream);
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
