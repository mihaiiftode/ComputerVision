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
            this.RotateButton = new System.Windows.Forms.Button();
            this.ScalingButton = new System.Windows.Forms.Button();
            this.HistogramaButton = new System.Windows.Forms.Button();
            this.ContrastTrackBar = new System.Windows.Forms.TrackBar();
            this.ContrastButton = new System.Windows.Forms.Button();
            this.BrightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.BrightnessButton = new System.Windows.Forms.Button();
            this.NegativePictureButton = new System.Windows.Forms.Button();
            this.GrayscaleButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ReflectionButton = new System.Windows.Forms.Button();
            this.TranslationButton = new System.Windows.Forms.Button();
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
            this.SourcePanel.Location = new System.Drawing.Point(24, 23);
            this.SourcePanel.Margin = new System.Windows.Forms.Padding(6);
            this.SourcePanel.Name = "SourcePanel";
            this.SourcePanel.Size = new System.Drawing.Size(636, 458);
            this.SourcePanel.TabIndex = 0;
            // 
            // DestinationPanel
            // 
            this.DestinationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DestinationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DestinationPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DestinationPanel.Location = new System.Drawing.Point(732, 23);
            this.DestinationPanel.Margin = new System.Windows.Forms.Padding(6);
            this.DestinationPanel.Name = "DestinationPanel";
            this.DestinationPanel.Size = new System.Drawing.Size(636, 458);
            this.DestinationPanel.TabIndex = 1;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(24, 583);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(6);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(150, 44);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TranslationButton);
            this.panel1.Controls.Add(this.ReflectionButton);
            this.panel1.Controls.Add(this.RotateButton);
            this.panel1.Controls.Add(this.ScalingButton);
            this.panel1.Controls.Add(this.HistogramaButton);
            this.panel1.Controls.Add(this.ContrastTrackBar);
            this.panel1.Controls.Add(this.ContrastButton);
            this.panel1.Controls.Add(this.BrightnessTrackBar);
            this.panel1.Controls.Add(this.BrightnessButton);
            this.panel1.Controls.Add(this.NegativePictureButton);
            this.panel1.Controls.Add(this.GrayscaleButton);
            this.panel1.Location = new System.Drawing.Point(186, 583);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1231, 331);
            this.panel1.TabIndex = 3;
            // 
            // RotateButton
            // 
            this.RotateButton.Location = new System.Drawing.Point(599, 160);
            this.RotateButton.Margin = new System.Windows.Forms.Padding(6);
            this.RotateButton.Name = "RotateButton";
            this.RotateButton.Size = new System.Drawing.Size(213, 44);
            this.RotateButton.TabIndex = 20;
            this.RotateButton.Text = "Rotate";
            this.RotateButton.UseVisualStyleBackColor = true;
            this.RotateButton.Click += new System.EventHandler(this.RotateButton_Click);
            // 
            // ScalingButton
            // 
            this.ScalingButton.Location = new System.Drawing.Point(599, 85);
            this.ScalingButton.Margin = new System.Windows.Forms.Padding(6);
            this.ScalingButton.Name = "ScalingButton";
            this.ScalingButton.Size = new System.Drawing.Size(213, 44);
            this.ScalingButton.TabIndex = 19;
            this.ScalingButton.Text = "Scale";
            this.ScalingButton.UseVisualStyleBackColor = true;
            this.ScalingButton.Click += new System.EventHandler(this.ScalingButton_Click);
            // 
            // HistogramaButton
            // 
            this.HistogramaButton.Location = new System.Drawing.Point(599, 15);
            this.HistogramaButton.Name = "HistogramaButton";
            this.HistogramaButton.Size = new System.Drawing.Size(213, 44);
            this.HistogramaButton.TabIndex = 18;
            this.HistogramaButton.Text = "Egali. Histograma";
            this.HistogramaButton.UseVisualStyleBackColor = true;
            this.HistogramaButton.Click += new System.EventHandler(this.HistogramaButton_Click);
            // 
            // ContrastTrackBar
            // 
            this.ContrastTrackBar.Location = new System.Drawing.Point(208, 240);
            this.ContrastTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.ContrastTrackBar.Maximum = 350;
            this.ContrastTrackBar.Minimum = -100;
            this.ContrastTrackBar.Name = "ContrastTrackBar";
            this.ContrastTrackBar.Size = new System.Drawing.Size(356, 90);
            this.ContrastTrackBar.TabIndex = 17;
            // 
            // ContrastButton
            // 
            this.ContrastButton.Location = new System.Drawing.Point(6, 248);
            this.ContrastButton.Margin = new System.Windows.Forms.Padding(6);
            this.ContrastButton.Name = "ContrastButton";
            this.ContrastButton.Size = new System.Drawing.Size(150, 44);
            this.ContrastButton.TabIndex = 4;
            this.ContrastButton.Text = "Contrast";
            this.ContrastButton.UseVisualStyleBackColor = true;
            this.ContrastButton.Click += new System.EventHandler(this.ContrastButton_Click);
            // 
            // BrightnessTrackBar
            // 
            this.BrightnessTrackBar.Location = new System.Drawing.Point(208, 152);
            this.BrightnessTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.BrightnessTrackBar.Maximum = 255;
            this.BrightnessTrackBar.Minimum = -255;
            this.BrightnessTrackBar.Name = "BrightnessTrackBar";
            this.BrightnessTrackBar.Size = new System.Drawing.Size(356, 90);
            this.BrightnessTrackBar.TabIndex = 16;
            this.BrightnessTrackBar.Scroll += new System.EventHandler(this.TrackBarBrightness_Scroll);
            // 
            // BrightnessButton
            // 
            this.BrightnessButton.Location = new System.Drawing.Point(6, 160);
            this.BrightnessButton.Margin = new System.Windows.Forms.Padding(6);
            this.BrightnessButton.Name = "BrightnessButton";
            this.BrightnessButton.Size = new System.Drawing.Size(150, 44);
            this.BrightnessButton.TabIndex = 15;
            this.BrightnessButton.Text = "Brightness";
            this.BrightnessButton.UseVisualStyleBackColor = true;
            this.BrightnessButton.Click += new System.EventHandler(this.BrightnessButton_Click);
            // 
            // NegativePictureButton
            // 
            this.NegativePictureButton.Location = new System.Drawing.Point(6, 85);
            this.NegativePictureButton.Margin = new System.Windows.Forms.Padding(6);
            this.NegativePictureButton.Name = "NegativePictureButton";
            this.NegativePictureButton.Size = new System.Drawing.Size(150, 44);
            this.NegativePictureButton.TabIndex = 14;
            this.NegativePictureButton.Text = "Negative";
            this.NegativePictureButton.UseVisualStyleBackColor = true;
            this.NegativePictureButton.Click += new System.EventHandler(this.NegativateButton_Click);
            // 
            // GrayscaleButton
            // 
            this.GrayscaleButton.Location = new System.Drawing.Point(6, 15);
            this.GrayscaleButton.Margin = new System.Windows.Forms.Padding(6);
            this.GrayscaleButton.Name = "GrayscaleButton";
            this.GrayscaleButton.Size = new System.Drawing.Size(150, 44);
            this.GrayscaleButton.TabIndex = 13;
            this.GrayscaleButton.Text = "Grayscale";
            this.GrayscaleButton.UseVisualStyleBackColor = true;
            this.GrayscaleButton.Click += new System.EventHandler(this.buttonGrayscale_Click);
            // 
            // ReflectionButton
            // 
            this.ReflectionButton.Location = new System.Drawing.Point(832, 15);
            this.ReflectionButton.Margin = new System.Windows.Forms.Padding(6);
            this.ReflectionButton.Name = "ReflectionButton";
            this.ReflectionButton.Size = new System.Drawing.Size(213, 44);
            this.ReflectionButton.TabIndex = 21;
            this.ReflectionButton.Text = "Reflexion";
            this.ReflectionButton.UseVisualStyleBackColor = true;
            this.ReflectionButton.Click += new System.EventHandler(this.ReflectionButton_Click);
            // 
            // TranslationButton
            // 
            this.TranslationButton.Location = new System.Drawing.Point(599, 240);
            this.TranslationButton.Margin = new System.Windows.Forms.Padding(6);
            this.TranslationButton.Name = "TranslationButton";
            this.TranslationButton.Size = new System.Drawing.Size(213, 44);
            this.TranslationButton.TabIndex = 22;
            this.TranslationButton.Text = "Translation";
            this.TranslationButton.UseVisualStyleBackColor = true;
            this.TranslationButton.Click += new System.EventHandler(this.TranslationButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 921);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.DestinationPanel);
            this.Controls.Add(this.SourcePanel);
            this.Margin = new System.Windows.Forms.Padding(6);
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
        private System.Windows.Forms.Button HistogramaButton;
        private System.Windows.Forms.Button ScalingButton;
        private System.Windows.Forms.Button RotateButton;
        private System.Windows.Forms.Button ReflectionButton;
        private System.Windows.Forms.Button TranslationButton;
    }
}

