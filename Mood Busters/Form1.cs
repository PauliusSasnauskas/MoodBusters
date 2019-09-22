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
        private IRecognitionApi apiClient;
        public Form1()
        {
            InitializeComponent();
            apiClient = new AmazonRekognitionApi();
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
                    label1.Text = apiClient.GetMood(imageLocation).ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
