namespace ComputerVision
{
    partial class MainForm
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
            this.SourcePanel = new System.Windows.Forms.Panel();
            this.DestinationPanel = new System.Windows.Forms.Panel();
            this.LoadButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ContrastTrackBar = new System.Windows.Forms.TrackBar();
            this.ContrastButton = new System.Windows.Forms.Button();
            this.BrightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.BrightnessButton = new System.Windows.Forms.Button();
            this.NegativePictureButton = new System.Windows.Forms.Button();
            this.GrayscaleButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SourcePanel
            // 
            this.SourcePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SourcePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SourcePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SourcePanel.Location = new System.Drawing.Point(12, 12);
            this.SourcePanel.Name = "SourcePanel";
            this.SourcePanel.Size = new System.Drawing.Size(320, 240);
            this.SourcePanel.TabIndex = 0;
            // 
            // DestinationPanel
            // 
            this.DestinationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DestinationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DestinationPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DestinationPanel.Location = new System.Drawing.Point(366, 12);
            this.DestinationPanel.Name = "DestinationPanel";
            this.DestinationPanel.Size = new System.Drawing.Size(320, 240);
            this.DestinationPanel.TabIndex = 1;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(12, 303);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ContrastTrackBar);
            this.panel1.Controls.Add(this.ContrastButton);
            this.panel1.Controls.Add(this.BrightnessTrackBar);
            this.panel1.Controls.Add(this.BrightnessButton);
            this.panel1.Controls.Add(this.NegativePictureButton);
            this.panel1.Controls.Add(this.GrayscaleButton);
            this.panel1.Location = new System.Drawing.Point(366, 294);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 173);
            this.panel1.TabIndex = 3;
            // 
            // ContrastTrackBar
            // 
            this.ContrastTrackBar.Location = new System.Drawing.Point(104, 125);
            this.ContrastTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ContrastTrackBar.Maximum = 350;
            this.ContrastTrackBar.Minimum = -100;
            this.ContrastTrackBar.Name = "ContrastTrackBar";
            this.ContrastTrackBar.Size = new System.Drawing.Size(178, 45);
            this.ContrastTrackBar.TabIndex = 17;
            // 
            // ContrastButton
            // 
            this.ContrastButton.Location = new System.Drawing.Point(3, 129);
            this.ContrastButton.Name = "ContrastButton";
            this.ContrastButton.Size = new System.Drawing.Size(75, 23);
            this.ContrastButton.TabIndex = 4;
            this.ContrastButton.Text = "Contrast";
            this.ContrastButton.UseVisualStyleBackColor = true;
            this.ContrastButton.Click += new System.EventHandler(this.ContrastButton_Click);
            // 
            // BrightnessTrackBar
            // 
            this.BrightnessTrackBar.Location = new System.Drawing.Point(104, 79);
            this.BrightnessTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BrightnessTrackBar.Maximum = 255;
            this.BrightnessTrackBar.Minimum = -255;
            this.BrightnessTrackBar.Name = "BrightnessTrackBar";
            this.BrightnessTrackBar.Size = new System.Drawing.Size(178, 45);
            this.BrightnessTrackBar.TabIndex = 16;
            this.BrightnessTrackBar.Scroll += new System.EventHandler(this.TrackBarBrightness_Scroll);
            // 
            // BrightnessButton
            // 
            this.BrightnessButton.Location = new System.Drawing.Point(3, 83);
            this.BrightnessButton.Name = "BrightnessButton";
            this.BrightnessButton.Size = new System.Drawing.Size(75, 23);
            this.BrightnessButton.TabIndex = 15;
            this.BrightnessButton.Text = "Brightness";
            this.BrightnessButton.UseVisualStyleBackColor = true;
            this.BrightnessButton.Click += new System.EventHandler(this.BrightnessButton_Click);
            // 
            // NegativePictureButton
            // 
            this.NegativePictureButton.Location = new System.Drawing.Point(3, 44);
            this.NegativePictureButton.Name = "NegativePictureButton";
            this.NegativePictureButton.Size = new System.Drawing.Size(75, 23);
            this.NegativePictureButton.TabIndex = 14;
            this.NegativePictureButton.Text = "Negative";
            this.NegativePictureButton.UseVisualStyleBackColor = true;
            this.NegativePictureButton.Click += new System.EventHandler(this.NegativateButton_Click);
            // 
            // GrayscaleButton
            // 
            this.GrayscaleButton.Location = new System.Drawing.Point(3, 8);
            this.GrayscaleButton.Name = "GrayscaleButton";
            this.GrayscaleButton.Size = new System.Drawing.Size(75, 23);
            this.GrayscaleButton.TabIndex = 13;
            this.GrayscaleButton.Text = "Grayscale";
            this.GrayscaleButton.UseVisualStyleBackColor = true;
            this.GrayscaleButton.Click += new System.EventHandler(this.buttonGrayscale_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 479);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.DestinationPanel);
            this.Controls.Add(this.SourcePanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SourcePanel;
        private System.Windows.Forms.Panel DestinationPanel;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button GrayscaleButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button NegativePictureButton;
        private System.Windows.Forms.TrackBar BrightnessTrackBar;
        private System.Windows.Forms.Button BrightnessButton;
        private System.Windows.Forms.Button ContrastButton;
        private System.Windows.Forms.TrackBar ContrastTrackBar;
    }
}

