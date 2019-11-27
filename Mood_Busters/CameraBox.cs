using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using MoodBustersLibrary;

namespace AndroMooda3
{
    class CameraBox : ICameraBox
    {
        private Capture capture;
        private PictureBox pictureBox;
        private bool streaming_off = false;

        public CameraBox(PictureBox pictureBox)
        {
            capture = new Capture();
            this.pictureBox = pictureBox;
        }

        public void StreamFrames(object sender, EventArgs e)
        {
            if (streaming_off) return;
            var img = capture.QueryFrame().ToImage<Bgr, byte>();
            pictureBox.Image = img.Bitmap;
        }

        public void StopCamera()
        {
            streaming_off = true;
        }

        public void ResumeCamera()
        {
            streaming_off = false;
        }
    }
}
