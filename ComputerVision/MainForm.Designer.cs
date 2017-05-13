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
            this.LoadButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MedianButton = new System.Windows.Forms.Button();
            this.OutlierButton = new System.Windows.Forms.Button();
            this.FtjUpDown = new System.Windows.Forms.NumericUpDown();
            this.FtjButton = new System.Windows.Forms.Button();
            this.TranslationButton = new System.Windows.Forms.Button();
            this.ReflectionButton = new System.Windows.Forms.Button();
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
            this.FTSButton = new System.Windows.Forms.Button();
            this.UnsharpMaskButton = new System.Windows.Forms.Button();
            this.KirschButton = new System.Windows.Forms.Button();
            this.Kirch2Button = new System.Windows.Forms.Button();
            this.LaplaceButton = new System.Windows.Forms.Button();
            this.RobertsButton = new System.Windows.Forms.Button();
            this.PrewittButton = new System.Windows.Forms.Button();
            this.SobelButton = new System.Windows.Forms.Button();
            this.PseudoMedianButton = new System.Windows.Forms.Button();
            this.SortIntensityButton = new System.Windows.Forms.Button();
            this.FreiChenButton = new System.Windows.Forms.Button();
            this.GaborButton = new System.Windows.Forms.Button();
            this.RegionGrowingButton = new System.Windows.Forms.Button();
            this.x = new System.Windows.Forms.TextBox();
            this.y = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.TextBox();
            this.CorelatieButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BlockMatchingButton = new System.Windows.Forms.Button();
            this.SourcePanel = new bambit.forms.controls.PictureBoxExtended();
            this.DestinationPanel = new bambit.forms.controls.PictureBoxExtended();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FtjUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestinationPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(16, 373);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(100, 28);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MedianButton);
            this.panel1.Controls.Add(this.OutlierButton);
            this.panel1.Controls.Add(this.FtjUpDown);
            this.panel1.Controls.Add(this.FtjButton);
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
            this.panel1.Location = new System.Drawing.Point(124, 373);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 213);
            this.panel1.TabIndex = 3;
            // 
            // MedianButton
            // 
            this.MedianButton.Location = new System.Drawing.Point(555, 154);
            this.MedianButton.Margin = new System.Windows.Forms.Padding(2);
            this.MedianButton.Name = "MedianButton";
            this.MedianButton.Size = new System.Drawing.Size(142, 28);
            this.MedianButton.TabIndex = 27;
            this.MedianButton.Text = "Median";
            this.MedianButton.UseVisualStyleBackColor = true;
            this.MedianButton.Click += new System.EventHandler(this.MedianButton_Click);
            // 
            // OutlierButton
            // 
            this.OutlierButton.Location = new System.Drawing.Point(555, 102);
            this.OutlierButton.Margin = new System.Windows.Forms.Padding(2);
            this.OutlierButton.Name = "OutlierButton";
            this.OutlierButton.Size = new System.Drawing.Size(142, 28);
            this.OutlierButton.TabIndex = 26;
            this.OutlierButton.Text = "Outlier";
            this.OutlierButton.UseVisualStyleBackColor = true;
            this.OutlierButton.Click += new System.EventHandler(this.OutlierButton_Click);
            // 
            // FtjUpDown
            // 
            this.FtjUpDown.Location = new System.Drawing.Point(714, 63);
            this.FtjUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.FtjUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FtjUpDown.Name = "FtjUpDown";
            this.FtjUpDown.Size = new System.Drawing.Size(80, 22);
            this.FtjUpDown.TabIndex = 25;
            // 
            // FtjButton
            // 
            this.FtjButton.Location = new System.Drawing.Point(555, 54);
            this.FtjButton.Margin = new System.Windows.Forms.Padding(2);
            this.FtjButton.Name = "FtjButton";
            this.FtjButton.Size = new System.Drawing.Size(142, 28);
            this.FtjButton.TabIndex = 23;
            this.FtjButton.Text = "FTJ";
            this.FtjButton.UseVisualStyleBackColor = true;
            this.FtjButton.Click += new System.EventHandler(this.FtjButton_Click);
            // 
            // TranslationButton
            // 
            this.TranslationButton.Location = new System.Drawing.Point(399, 154);
            this.TranslationButton.Margin = new System.Windows.Forms.Padding(4);
            this.TranslationButton.Name = "TranslationButton";
            this.TranslationButton.Size = new System.Drawing.Size(142, 28);
            this.TranslationButton.TabIndex = 22;
            this.TranslationButton.Text = "Translation";
            this.TranslationButton.UseVisualStyleBackColor = true;
            this.TranslationButton.Click += new System.EventHandler(this.TranslationButton_Click);
            // 
            // ReflectionButton
            // 
            this.ReflectionButton.Location = new System.Drawing.Point(555, 10);
            this.ReflectionButton.Margin = new System.Windows.Forms.Padding(4);
            this.ReflectionButton.Name = "ReflectionButton";
            this.ReflectionButton.Size = new System.Drawing.Size(142, 28);
            this.ReflectionButton.TabIndex = 21;
            this.ReflectionButton.Text = "Reflexion";
            this.ReflectionButton.UseVisualStyleBackColor = true;
            this.ReflectionButton.Click += new System.EventHandler(this.ReflectionButton_Click);
            // 
            // RotateButton
            // 
            this.RotateButton.Location = new System.Drawing.Point(399, 102);
            this.RotateButton.Margin = new System.Windows.Forms.Padding(4);
            this.RotateButton.Name = "RotateButton";
            this.RotateButton.Size = new System.Drawing.Size(142, 28);
            this.RotateButton.TabIndex = 20;
            this.RotateButton.Text = "Rotate";
            this.RotateButton.UseVisualStyleBackColor = true;
            this.RotateButton.Click += new System.EventHandler(this.RotateButton_Click);
            // 
            // ScalingButton
            // 
            this.ScalingButton.Location = new System.Drawing.Point(399, 54);
            this.ScalingButton.Margin = new System.Windows.Forms.Padding(4);
            this.ScalingButton.Name = "ScalingButton";
            this.ScalingButton.Size = new System.Drawing.Size(142, 28);
            this.ScalingButton.TabIndex = 19;
            this.ScalingButton.Text = "Scale";
            this.ScalingButton.UseVisualStyleBackColor = true;
            this.ScalingButton.Click += new System.EventHandler(this.ScalingButton_Click);
            // 
            // HistogramaButton
            // 
            this.HistogramaButton.Location = new System.Drawing.Point(399, 10);
            this.HistogramaButton.Margin = new System.Windows.Forms.Padding(2);
            this.HistogramaButton.Name = "HistogramaButton";
            this.HistogramaButton.Size = new System.Drawing.Size(142, 28);
            this.HistogramaButton.TabIndex = 18;
            this.HistogramaButton.Text = "Egali. Histograma";
            this.HistogramaButton.UseVisualStyleBackColor = true;
            this.HistogramaButton.Click += new System.EventHandler(this.HistogramaButton_Click);
            // 
            // ContrastTrackBar
            // 
            this.ContrastTrackBar.Location = new System.Drawing.Point(139, 154);
            this.ContrastTrackBar.Maximum = 350;
            this.ContrastTrackBar.Minimum = -100;
            this.ContrastTrackBar.Name = "ContrastTrackBar";
            this.ContrastTrackBar.Size = new System.Drawing.Size(237, 56);
            this.ContrastTrackBar.TabIndex = 17;
            // 
            // ContrastButton
            // 
            this.ContrastButton.Location = new System.Drawing.Point(4, 159);
            this.ContrastButton.Margin = new System.Windows.Forms.Padding(4);
            this.ContrastButton.Name = "ContrastButton";
            this.ContrastButton.Size = new System.Drawing.Size(100, 28);
            this.ContrastButton.TabIndex = 4;
            this.ContrastButton.Text = "Contrast";
            this.ContrastButton.UseVisualStyleBackColor = true;
            this.ContrastButton.Click += new System.EventHandler(this.ContrastButton_Click);
            // 
            // BrightnessTrackBar
            // 
            this.BrightnessTrackBar.Location = new System.Drawing.Point(139, 97);
            this.BrightnessTrackBar.Maximum = 255;
            this.BrightnessTrackBar.Minimum = -255;
            this.BrightnessTrackBar.Name = "BrightnessTrackBar";
            this.BrightnessTrackBar.Size = new System.Drawing.Size(237, 56);
            this.BrightnessTrackBar.TabIndex = 16;
            this.BrightnessTrackBar.Scroll += new System.EventHandler(this.TrackBarBrightness_Scroll);
            // 
            // BrightnessButton
            // 
            this.BrightnessButton.Location = new System.Drawing.Point(4, 102);
            this.BrightnessButton.Margin = new System.Windows.Forms.Padding(4);
            this.BrightnessButton.Name = "BrightnessButton";
            this.BrightnessButton.Size = new System.Drawing.Size(100, 28);
            this.BrightnessButton.TabIndex = 15;
            this.BrightnessButton.Text = "Brightness";
            this.BrightnessButton.UseVisualStyleBackColor = true;
            this.BrightnessButton.Click += new System.EventHandler(this.BrightnessButton_Click);
            // 
            // NegativePictureButton
            // 
            this.NegativePictureButton.Location = new System.Drawing.Point(4, 54);
            this.NegativePictureButton.Margin = new System.Windows.Forms.Padding(4);
            this.NegativePictureButton.Name = "NegativePictureButton";
            this.NegativePictureButton.Size = new System.Drawing.Size(100, 28);
            this.NegativePictureButton.TabIndex = 14;
            this.NegativePictureButton.Text = "Negative";
            this.NegativePictureButton.UseVisualStyleBackColor = true;
            this.NegativePictureButton.Click += new System.EventHandler(this.NegativateButton_Click);
            // 
            // GrayscaleButton
            // 
            this.GrayscaleButton.Location = new System.Drawing.Point(4, 10);
            this.GrayscaleButton.Margin = new System.Windows.Forms.Padding(4);
            this.GrayscaleButton.Name = "GrayscaleButton";
            this.GrayscaleButton.Size = new System.Drawing.Size(100, 28);
            this.GrayscaleButton.TabIndex = 13;
            this.GrayscaleButton.Text = "Grayscale";
            this.GrayscaleButton.UseVisualStyleBackColor = true;
            this.GrayscaleButton.Click += new System.EventHandler(this.buttonGrayscale_Click);
            // 
            // FTSButton
            // 
            this.FTSButton.Location = new System.Drawing.Point(918, 15);
            this.FTSButton.Margin = new System.Windows.Forms.Padding(2);
            this.FTSButton.Name = "FTSButton";
            this.FTSButton.Size = new System.Drawing.Size(142, 28);
            this.FTSButton.TabIndex = 28;
            this.FTSButton.Text = "FTS";
            this.FTSButton.UseVisualStyleBackColor = true;
            this.FTSButton.Click += new System.EventHandler(this.FTSButton_Click);
            // 
            // UnsharpMaskButton
            // 
            this.UnsharpMaskButton.Location = new System.Drawing.Point(918, 63);
            this.UnsharpMaskButton.Margin = new System.Windows.Forms.Padding(2);
            this.UnsharpMaskButton.Name = "UnsharpMaskButton";
            this.UnsharpMaskButton.Size = new System.Drawing.Size(142, 28);
            this.UnsharpMaskButton.TabIndex = 29;
            this.UnsharpMaskButton.Text = "Unsharp Mask";
            this.UnsharpMaskButton.UseVisualStyleBackColor = true;
            this.UnsharpMaskButton.Click += new System.EventHandler(this.UnsharpMaskButton_Click);
            // 
            // KirschButton
            // 
            this.KirschButton.Location = new System.Drawing.Point(918, 115);
            this.KirschButton.Margin = new System.Windows.Forms.Padding(2);
            this.KirschButton.Name = "KirschButton";
            this.KirschButton.Size = new System.Drawing.Size(142, 28);
            this.KirschButton.TabIndex = 30;
            this.KirschButton.Text = "Kirsch 1";
            this.KirschButton.UseVisualStyleBackColor = true;
            this.KirschButton.Click += new System.EventHandler(this.KirschButton_Click);
            // 
            // Kirch2Button
            // 
            this.Kirch2Button.Location = new System.Drawing.Point(918, 164);
            this.Kirch2Button.Margin = new System.Windows.Forms.Padding(2);
            this.Kirch2Button.Name = "Kirch2Button";
            this.Kirch2Button.Size = new System.Drawing.Size(142, 28);
            this.Kirch2Button.TabIndex = 31;
            this.Kirch2Button.Text = "Kirsch 2";
            this.Kirch2Button.UseVisualStyleBackColor = true;
            this.Kirch2Button.Click += new System.EventHandler(this.Kirch2Button_Click);
            // 
            // LaplaceButton
            // 
            this.LaplaceButton.Location = new System.Drawing.Point(918, 209);
            this.LaplaceButton.Margin = new System.Windows.Forms.Padding(2);
            this.LaplaceButton.Name = "LaplaceButton";
            this.LaplaceButton.Size = new System.Drawing.Size(142, 28);
            this.LaplaceButton.TabIndex = 32;
            this.LaplaceButton.Text = "Laplace";
            this.LaplaceButton.UseVisualStyleBackColor = true;
            this.LaplaceButton.Click += new System.EventHandler(this.LaplaceButton_Click);
            // 
            // RobertsButton
            // 
            this.RobertsButton.Location = new System.Drawing.Point(918, 250);
            this.RobertsButton.Margin = new System.Windows.Forms.Padding(2);
            this.RobertsButton.Name = "RobertsButton";
            this.RobertsButton.Size = new System.Drawing.Size(142, 28);
            this.RobertsButton.TabIndex = 33;
            this.RobertsButton.Text = "Roberts";
            this.RobertsButton.UseVisualStyleBackColor = true;
            this.RobertsButton.Click += new System.EventHandler(this.RobertsButton_Click);
            // 
            // PrewittButton
            // 
            this.PrewittButton.Location = new System.Drawing.Point(918, 294);
            this.PrewittButton.Margin = new System.Windows.Forms.Padding(2);
            this.PrewittButton.Name = "PrewittButton";
            this.PrewittButton.Size = new System.Drawing.Size(142, 28);
            this.PrewittButton.TabIndex = 34;
            this.PrewittButton.Text = "Prewitt";
            this.PrewittButton.UseVisualStyleBackColor = true;
            this.PrewittButton.Click += new System.EventHandler(this.PrewittButton_Click);
            // 
            // SobelButton
            // 
            this.SobelButton.Location = new System.Drawing.Point(918, 339);
            this.SobelButton.Margin = new System.Windows.Forms.Padding(2);
            this.SobelButton.Name = "SobelButton";
            this.SobelButton.Size = new System.Drawing.Size(142, 28);
            this.SobelButton.TabIndex = 35;
            this.SobelButton.Text = "Sobel";
            this.SobelButton.UseVisualStyleBackColor = true;
            this.SobelButton.Click += new System.EventHandler(this.SobelButton_Click);
            // 
            // PseudoMedianButton
            // 
            this.PseudoMedianButton.Location = new System.Drawing.Point(1066, 15);
            this.PseudoMedianButton.Margin = new System.Windows.Forms.Padding(4);
            this.PseudoMedianButton.Name = "PseudoMedianButton";
            this.PseudoMedianButton.Size = new System.Drawing.Size(142, 28);
            this.PseudoMedianButton.TabIndex = 36;
            this.PseudoMedianButton.Text = "PseudoMedian";
            this.PseudoMedianButton.UseVisualStyleBackColor = true;
            this.PseudoMedianButton.Click += new System.EventHandler(this.PseudoMedianButton_Click);
            // 
            // SortIntensityButton
            // 
            this.SortIntensityButton.Location = new System.Drawing.Point(1066, 63);
            this.SortIntensityButton.Margin = new System.Windows.Forms.Padding(4);
            this.SortIntensityButton.Name = "SortIntensityButton";
            this.SortIntensityButton.Size = new System.Drawing.Size(142, 28);
            this.SortIntensityButton.TabIndex = 37;
            this.SortIntensityButton.Text = "SortIntensityButton";
            this.SortIntensityButton.UseVisualStyleBackColor = true;
            this.SortIntensityButton.Click += new System.EventHandler(this.SortIntensityButton_Click);
            // 
            // FreiChenButton
            // 
            this.FreiChenButton.Location = new System.Drawing.Point(1066, 115);
            this.FreiChenButton.Margin = new System.Windows.Forms.Padding(4);
            this.FreiChenButton.Name = "FreiChenButton";
            this.FreiChenButton.Size = new System.Drawing.Size(142, 28);
            this.FreiChenButton.TabIndex = 38;
            this.FreiChenButton.Text = "FreiChen";
            this.FreiChenButton.UseVisualStyleBackColor = true;
            this.FreiChenButton.Click += new System.EventHandler(this.FreiChenButton_Click);
            // 
            // GaborButton
            // 
            this.GaborButton.Location = new System.Drawing.Point(1066, 164);
            this.GaborButton.Margin = new System.Windows.Forms.Padding(4);
            this.GaborButton.Name = "GaborButton";
            this.GaborButton.Size = new System.Drawing.Size(142, 28);
            this.GaborButton.TabIndex = 39;
            this.GaborButton.Text = "Gabor";
            this.GaborButton.UseVisualStyleBackColor = true;
            this.GaborButton.Click += new System.EventHandler(this.GaborButton_Click);
            // 
            // RegionGrowingButton
            // 
            this.RegionGrowingButton.Location = new System.Drawing.Point(1066, 209);
            this.RegionGrowingButton.Margin = new System.Windows.Forms.Padding(4);
            this.RegionGrowingButton.Name = "RegionGrowingButton";
            this.RegionGrowingButton.Size = new System.Drawing.Size(142, 28);
            this.RegionGrowingButton.TabIndex = 41;
            this.RegionGrowingButton.Text = "Region Growing";
            this.RegionGrowingButton.UseVisualStyleBackColor = true;
            this.RegionGrowingButton.Click += new System.EventHandler(this.RegionGrowingButton_Click);
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(1117, 256);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(70, 22);
            this.x.TabIndex = 42;
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(1117, 294);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(70, 22);
            this.y.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1063, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 44;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1063, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 45;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1063, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 47;
            this.label3.Text = "value";
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(1117, 339);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(70, 22);
            this.value.TabIndex = 46;
            // 
            // CorelatieButton
            // 
            this.CorelatieButton.Location = new System.Drawing.Point(918, 384);
            this.CorelatieButton.Margin = new System.Windows.Forms.Padding(4);
            this.CorelatieButton.Name = "CorelatieButton";
            this.CorelatieButton.Size = new System.Drawing.Size(142, 28);
            this.CorelatieButton.TabIndex = 49;
            this.CorelatieButton.Text = "Correlation";
            this.CorelatieButton.UseVisualStyleBackColor = true;
            this.CorelatieButton.Click += new System.EventHandler(this.CorelatieButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 428);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 50;
            this.button1.Text = "Load Region Matching";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadBlockMatching_Click);
            // 
            // BlockMatchingButton
            // 
            this.BlockMatchingButton.Location = new System.Drawing.Point(1066, 384);
            this.BlockMatchingButton.Margin = new System.Windows.Forms.Padding(4);
            this.BlockMatchingButton.Name = "BlockMatchingButton";
            this.BlockMatchingButton.Size = new System.Drawing.Size(142, 28);
            this.BlockMatchingButton.TabIndex = 51;
            this.BlockMatchingButton.Text = "b";
            this.BlockMatchingButton.UseVisualStyleBackColor = true;
            this.BlockMatchingButton.Click += new System.EventHandler(this.BlockMatchingButton_Click);
            // 
            // SourcePanel
            // 
            this.SourcePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SourcePanel.Location = new System.Drawing.Point(2, 12);
            this.SourcePanel.Name = "SourcePanel";
            this.SourcePanel.Size = new System.Drawing.Size(440, 298);
            this.SourcePanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SourcePanel.TabIndex = 48;
            this.SourcePanel.TabStop = false;
            this.SourcePanel.Click += new System.EventHandler(this.SourcePanel_Click);
            // 
            // DestinationPanel
            // 
            this.DestinationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DestinationPanel.Location = new System.Drawing.Point(448, 15);
            this.DestinationPanel.Name = "DestinationPanel";
            this.DestinationPanel.Size = new System.Drawing.Size(455, 295);
            this.DestinationPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DestinationPanel.TabIndex = 40;
            this.DestinationPanel.TabStop = false;
            this.DestinationPanel.Click += new System.EventHandler(this.DestinationPanel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 603);
            this.Controls.Add(this.BlockMatchingButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CorelatieButton);
            this.Controls.Add(this.SourcePanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.value);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.y);
            this.Controls.Add(this.x);
            this.Controls.Add(this.RegionGrowingButton);
            this.Controls.Add(this.DestinationPanel);
            this.Controls.Add(this.GaborButton);
            this.Controls.Add(this.FreiChenButton);
            this.Controls.Add(this.SortIntensityButton);
            this.Controls.Add(this.PseudoMedianButton);
            this.Controls.Add(this.SobelButton);
            this.Controls.Add(this.PrewittButton);
            this.Controls.Add(this.RobertsButton);
            this.Controls.Add(this.LaplaceButton);
            this.Controls.Add(this.Kirch2Button);
            this.Controls.Add(this.KirschButton);
            this.Controls.Add(this.UnsharpMaskButton);
            this.Controls.Add(this.FTSButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LoadButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FtjUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestinationPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button FtjButton;
        private System.Windows.Forms.NumericUpDown FtjUpDown;
        private System.Windows.Forms.Button OutlierButton;
        private System.Windows.Forms.Button MedianButton;
        private System.Windows.Forms.Button FTSButton;
        private System.Windows.Forms.Button UnsharpMaskButton;
        private System.Windows.Forms.Button KirschButton;
        private System.Windows.Forms.Button Kirch2Button;
        private System.Windows.Forms.Button LaplaceButton;
        private System.Windows.Forms.Button RobertsButton;
        private System.Windows.Forms.Button PrewittButton;
        private System.Windows.Forms.Button SobelButton;
        private System.Windows.Forms.Button PseudoMedianButton;
        private System.Windows.Forms.Button SortIntensityButton;
        private System.Windows.Forms.Button FreiChenButton;
        private System.Windows.Forms.Button GaborButton;
        private bambit.forms.controls.PictureBoxExtended DestinationPanel;
        private System.Windows.Forms.Button RegionGrowingButton;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.TextBox y;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox value;
        private bambit.forms.controls.PictureBoxExtended SourcePanel;
        private System.Windows.Forms.Button CorelatieButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BlockMatchingButton;
    }
}

