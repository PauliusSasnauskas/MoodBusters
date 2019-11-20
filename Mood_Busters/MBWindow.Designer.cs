namespace AndroMooda3
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
            this.redButton = new System.Windows.Forms.Button();
            this.colorLabel = new System.Windows.Forms.Label();
            this.blueButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.analyzedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // analyzedImageBox
            // 
            this.analyzedImageBox.Location = new System.Drawing.Point(0, 0);
            this.analyzedImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.analyzedImageBox.Name = "analyzedImageBox";
            this.analyzedImageBox.Size = new System.Drawing.Size(430, 471);
            this.analyzedImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.analyzedImageBox.TabIndex = 0;
            this.analyzedImageBox.TabStop = false;
            // 
            // uploadButton
            // 
            this.uploadButton.BackgroundImage = global::AndroMooda3.Properties.Resources.browse;
            this.uploadButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.uploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadButton.ForeColor = System.Drawing.Color.Black;
            this.uploadButton.Location = new System.Drawing.Point(37, 496);
            this.uploadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(59, 53);
            this.uploadButton.TabIndex = 1;
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // getMoodButton
            // 
            this.getMoodButton.BackgroundImage = global::AndroMooda3.Properties.Resources.take_picture;
            this.getMoodButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.getMoodButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getMoodButton.ForeColor = System.Drawing.Color.Black;
            this.getMoodButton.Location = new System.Drawing.Point(183, 489);
            this.getMoodButton.Name = "getMoodButton";
            this.getMoodButton.Size = new System.Drawing.Size(75, 67);
            this.getMoodButton.TabIndex = 6;
            this.getMoodButton.UseVisualStyleBackColor = true;
            this.getMoodButton.Click += new System.EventHandler(this.GetMoodButtonClick);
            // 
            // saveButton
            // 
            this.saveButton.BackgroundImage = global::AndroMooda3.Properties.Resources.save;
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.Color.Black;
            this.saveButton.Location = new System.Drawing.Point(341, 496);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(58, 53);
            this.saveButton.TabIndex = 7;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // redButton
            // 
            this.redButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.redButton.Font = new System.Drawing.Font("Copperplate Gothic Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.redButton.Location = new System.Drawing.Point(18, 419);
            this.redButton.Name = "redButton";
            this.redButton.Size = new System.Drawing.Size(104, 52);
            this.redButton.TabIndex = 8;
            this.redButton.Text = "Red";
            this.redButton.UseVisualStyleBackColor = false;
            this.redButton.Click += new System.EventHandler(this.redButton_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.BackColor = System.Drawing.Color.Transparent;
            this.colorLabel.Font = new System.Drawing.Font("Old English Text MT", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorLabel.ForeColor = System.Drawing.Color.DarkViolet;
            this.colorLabel.Location = new System.Drawing.Point(165, 425);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(111, 33);
            this.colorLabel.TabIndex = 9;
            this.colorLabel.Text = "initel D";
            this.colorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blueButton
            // 
            this.blueButton.BackColor = System.Drawing.Color.Maroon;
            this.blueButton.Font = new System.Drawing.Font("Copperplate Gothic Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blueButton.ForeColor = System.Drawing.Color.Red;
            this.blueButton.Location = new System.Drawing.Point(295, 419);
            this.blueButton.Name = "blueButton";
            this.blueButton.Size = new System.Drawing.Size(104, 52);
            this.blueButton.TabIndex = 10;
            this.blueButton.Text = "Blue";
            this.blueButton.UseVisualStyleBackColor = false;
            this.blueButton.Click += new System.EventHandler(this.blueButton_Click);
            // 
            // MBWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(430, 575);
            this.Controls.Add(this.blueButton);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.redButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.getMoodButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.analyzedImageBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MBWindow";
            this.Text = "Mood Buster";
            this.Load += new System.EventHandler(this.MBWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.analyzedImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox analyzedImageBox;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button getMoodButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button redButton;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Button blueButton;
    }
}

