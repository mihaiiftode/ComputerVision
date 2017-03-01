using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ComputerVision
{
    public partial class MainForm : Form
    {
        private string _sourceFileName;
        private Bitmap _image;
        private Bitmap _brightnessBitmap;
        private FastImage _workImage;
        private FastImage _brightnessWorkImage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            _sourceFileName = openFileDialog.FileName;
            SourcePanel.BackgroundImage = new Bitmap(_sourceFileName);
            _image = new Bitmap(_sourceFileName);
            _brightnessBitmap = new Bitmap(_sourceFileName);
            _workImage = new FastImage(_image);
            _brightnessWorkImage = new FastImage(_brightnessBitmap);
        }

        private void buttonGrayscale_Click(object sender, EventArgs e)
        {
            _workImage.Lock();
            for (var i = 0; i < _workImage.Width; i++)
            {
                for (var j = 0; j < _workImage.Height; j++)
                {
                    var color = _workImage.GetPixel(i, j);
                    var r = color.R;
                    var g = color.G;
                    var b = color.B;

                    var average = (byte) ((r + g + b)/3);

                    color = Color.FromArgb(average, average, average);

                    _workImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _workImage.GetBitMap();
            _workImage.Unlock();
        }

        private void NegativateButton_Click(object sender, EventArgs e)
        {
            _workImage.Lock();
            for (var i = 0; i < _workImage.Width; i++)
            {
                for (var j = 0; j < _workImage.Height; j++)
                {
                    var color = _workImage.GetPixel(i, j);
                    var r = (byte) (byte.MaxValue - color.R);
                    var g = (byte) (byte.MaxValue - color.G);
                    var b = (byte) (byte.MaxValue - color.B);

                    var average = (byte) ((r + g + b)/3);

                    color = Color.FromArgb(average, average, average);

                    _workImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _workImage.GetBitMap();
            _workImage.Unlock();
        }

        private void BrightnessButton_Click(object sender, EventArgs e)
        {
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _brightnessWorkImage.GetBitMap();
        }

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            var delta = BrightnessTrackBar.Value;
            _brightnessWorkImage.Lock();
            _workImage.Lock();
            for (var i = 0; i < _workImage.Width; i++)
            {
                for (var j = 0; j < _workImage.Height; j++)
                {
                    var color = _workImage.GetPixel(i, j);

                    var r = (byte) (color.R + delta);
                    var g = (byte) (color.G + delta);
                    var b = (byte) (color.B + delta);


                    if (color.R + delta > 255) r = byte.MaxValue;
                    else if (color.R + delta < 0) r = byte.MinValue;
                    if (color.G + delta > 255) g = byte.MaxValue;
                    else if (color.G + delta < 0) g = byte.MinValue;
                    if (color.B + delta > 255) b = byte.MaxValue;
                    else if (color.B + delta < 0) b = byte.MinValue;

                    color = Color.FromArgb(r, g, b);

                    _brightnessWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _brightnessWorkImage.GetBitMap();
            _brightnessWorkImage.Unlock();
            _workImage.Unlock();
        }

        private void ContrastButton_Click(object sender, EventArgs e)
        {
            var minR = byte.MaxValue;
            var minG = byte.MaxValue;
            var minB = byte.MaxValue;

            var maxR = byte.MinValue;
            var maxG = byte.MinValue;
            var maxB = byte.MinValue;

            _workImage.Lock();
            for (var i = 0; i < _workImage.Width; i++)
            {
                for (var j = 0; j < _workImage.Height; j++)
                {
                    var color = _workImage.GetPixel(i, j);

                    if (color.R < minR) minR = color.R;
                    if (color.B < minB) minB = color.B;
                    if (color.G < minG) minG = color.G;

                    if (color.R > maxR) maxR = color.R;
                    if (color.B > maxB) maxB = color.B;
                    if (color.G > maxG) maxG = color.G;
                }
            }
            _workImage.Unlock();

            var delta = ContrastTrackBar.Value;

            int aR =  (minR - delta);
            int bR =  (maxR + delta);

            int aB =  (minB - delta);
            int bB =  (maxB + delta);

            int aG =  (minG - delta);
            int bG =  (maxG + delta);

            _brightnessWorkImage.Lock();
            _workImage.Lock();
            for (var i = 0; i < _workImage.Width; i++)
            {
                for (var j = 0; j < _workImage.Height; j++)
                {
                    var color = _workImage.GetPixel(i, j);

                    var r = (((bR - aR)*(color.R - minR))/(maxR - minR) + aR);
                    var b = (((bB - aB)*(color.B - minB))/(maxB - minB) + aB);
                    var g = (((bG - aG)*(color.G - minG))/(maxG - minG) + aG);

                    if (r > 255) r = byte.MaxValue;
                    else if (r < 0) r = byte.MinValue;
                    if (g > 255) g = byte.MaxValue;
                    else if (g < 0) g = byte.MinValue;
                    if (b > 255) b = byte.MaxValue;
                    else if (b < 0) b = byte.MinValue;

                    color = Color.FromArgb(r, g, b);

                    _brightnessWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _brightnessWorkImage.GetBitMap();
            _brightnessWorkImage.Unlock();
            _workImage.Unlock();
        }
    }
}