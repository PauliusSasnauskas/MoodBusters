namespace Mood_Busters
{ 
    partial class Form1
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
            this.analisedImageBox = new System.Windows.Forms.PictureBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.moodLabel = new System.Windows.Forms.Label();
            this.getMoodButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.analisedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // analisedImageBox
            // 
            this.analisedImageBox.Location = new System.Drawing.Point(11, 10);
            this.analisedImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.analisedImageBox.Name = "analisedImageBox";
            this.analisedImageBox.Size = new System.Drawing.Size(402, 461);
            this.analisedImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.analisedImageBox.TabIndex = 0;
            this.analisedImageBox.TabStop = false;
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
            // moodLabel
            // 
            this.moodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moodLabel.AutoSize = true;
            this.moodLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moodLabel.Font = new System.Drawing.Font("Papyrus", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moodLabel.Location = new System.Drawing.Point(138, 519);
            this.moodLabel.Name = "moodLabel";
            this.moodLabel.Size = new System.Drawing.Size(145, 22);
            this.moodLabel.TabIndex = 9;
            this.moodLabel.Text = "[MOOD LABEL]";
            this.moodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 680);
            this.Controls.Add(this.moodLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.getMoodButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.analisedImageBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Mood Buster";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.analisedImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox analisedImageBox;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button getMoodButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label moodLabel;
    }
}

