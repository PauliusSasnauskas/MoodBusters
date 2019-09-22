﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;

namespace Mood_Busters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string imageLocation;

        private void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; //Skirtas atrinkti reikiamus file extensionus
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    streaming_off = true;
                    imageLocation = dialog.FileName;
                    analisedImageBox.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not process the image.", "Error_processing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Capture capture;

        private void Form1_Load(object sender, EventArgs e)
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
                if (!streaming_off)
                {
                    MemoryStream memStream = new MemoryStream();
                    analisedImageBox.Image.Save(memStream, ImageFormat.Jpeg);
                    //label1.Text = DetectEmotion.getEmotion(memStream);            //DISABLED TO SAVE PAULIUXAS00'S MONEY(enable for testing when pressing button less than 1000 times)
                    moodLabel.Text = "TEST MODE";                                      //DELETE WHEN UNCOMMENTING THE LINE ABOVE
                    streaming_off = true;
                }
                else moodLabel.Text = DetectEmotion.GetEmotionFromFile(imageLocation);

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

    public class DetectEmotion
    {
        public static string GetEmotionFromFile(string path)
        {
            String photo = path;
            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    //return getEmotion(new MemoryStream(data));                //DISABLED TO SAVE PAULIUXAS00'S MONEY(enable for testing when pressing button less than 1000 times)
                    return "TEST MODE";                                         //DELETE WHEN UNCOMMENTING THE LINE ABOVE
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load file " + photo);
                return "Failed";
            }
        }

        public static String GetEmotion(MemoryStream memStr)
        {
            Image image = new Image();
            image.Bytes = memStr;

            var credentials = new Amazon.Runtime.BasicAWSCredentials(Key.GetKeys[0], Key.GetKeys[1]);
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(credentials, RegionEndpoint.EUCentral1);

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = image,
                Attributes = new List<String>() { "ALL" }
            };

            try
            {
                String highestConfidenceEmotion = "";
                float highestConfidence = 0.0f;
                DetectFacesResponse detectFacesResponse = rekognitionClient.DetectFaces(detectFacesRequest);
                foreach (FaceDetail face in detectFacesResponse.FaceDetails)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (face.Emotions[i].Confidence > highestConfidence)
                        {
                            highestConfidence = face.Emotions[i].Confidence;
                            highestConfidenceEmotion = face.Emotions[i].Type;
                        }
                    }
                }

                return highestConfidenceEmotion;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Failed";
        }
    }
}
