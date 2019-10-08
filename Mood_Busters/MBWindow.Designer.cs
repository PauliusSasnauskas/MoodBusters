namespace Mood_Busters
{ 
    partial class MBWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.analyzedImageBox = new System.Windows.Forms.PictureBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.getMoodButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.analyzedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // analyzedImageBox
            // 
            this.analyzedImageBox.Location = new System.Drawing.Point(11, 10);
            this.analyzedImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.analyzedImageBox.Name = "analyzedImageBox";
            this.analyzedImageBox.Size = new System.Drawing.Size(402, 461);
            this.analyzedImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.analyzedImageBox.TabIndex = 0;
            this.analyzedImageBox.TabStop = false;
            // 
            // uploadButton
            // 
            this.uploadButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uploadButton.Location = new System.Drawing.Point(39, 496);
            this.uploadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(88, 66);
            this.uploadButton.TabIndex = 1;
            this.uploadButton.Text = "Upload Photo";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // getMoodButton
            // 
            this.getMoodButton.Location = new System.Drawing.Point(150, 586);
            this.getMoodButton.Name = "getMoodButton";
            this.getMoodButton.Size = new System.Drawing.Size(124, 64);
            this.getMoodButton.TabIndex = 6;
            this.getMoodButton.Text = "Get Mood";
            this.getMoodButton.UseVisualStyleBackColor = true;
            this.getMoodButton.Click += new System.EventHandler(this.GetMoodButtonClick);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(295, 496);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(88, 66);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save Image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // MBWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 680);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.getMoodButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.analyzedImageBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MBWindow";
            this.Text = "Mood Buster";
            this.Load += new System.EventHandler(this.MBWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.analyzedImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox analyzedImageBox;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button getMoodButton;
        private System.Windows.Forms.Button saveButton;
    }
}

