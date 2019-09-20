﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace Mood_Busters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; //Skirtas atrinkti reikiamus file extensionus
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pictureBox1.ImageLocation = imageLocation;
                    label1.Text = DetectEmotion.getEmotion(imageLocation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not process the image.", "Error_processing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class DetectEmotion
    {
        public static String getEmotion(String path)
        {
            String photo = path;

            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();
            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load file " + photo);
                return "Failed";
            }

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
