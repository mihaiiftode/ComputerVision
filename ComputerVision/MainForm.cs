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
        private Bitmap _firstImage;
        private Bitmap _secondImage;
        private FastImage _firstWorkImage;
        private FastImage _secondWorkImage;


        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            _sourceFileName = openFileDialog.FileName;
            SourcePanel.BackgroundImage = new Bitmap(_sourceFileName);
            _firstImage = new Bitmap(_sourceFileName);
            _secondImage = new Bitmap(_sourceFileName);
            _firstWorkImage = new FastImage(_firstImage);
            _secondWorkImage = new FastImage(_secondImage);
        }

        private void buttonGrayscale_Click(object sender, EventArgs e)
        {
            _firstWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var color = _firstWorkImage.GetPixel(i, j);
                    var r = color.R;
                    var g = color.G;
                    var b = color.B;

                    var average = (byte)((r + g + b) / 3);

                    color = Color.FromArgb(average, average, average);

                    _firstWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _firstWorkImage.GetBitMap();
            _firstWorkImage.Unlock();
        }

        private void NegativateButton_Click(object sender, EventArgs e)
        {
            _firstWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var color = _firstWorkImage.GetPixel(i, j);
                    var r = (byte)(byte.MaxValue - color.R);
                    var g = (byte)(byte.MaxValue - color.G);
                    var b = (byte)(byte.MaxValue - color.B);

                    var average = (byte)((r + g + b) / 3);

                    color = Color.FromArgb(average, average, average);

                    _firstWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _firstWorkImage.GetBitMap();
            _firstWorkImage.Unlock();
        }

        private void BrightnessButton_Click(object sender, EventArgs e)
        {
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
        }

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            var delta = BrightnessTrackBar.Value;
            _secondWorkImage.Lock();
            _firstWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var color = _firstWorkImage.GetPixel(i, j);

                    var r = (byte)(color.R + delta);
                    var g = (byte)(color.G + delta);
                    var b = (byte)(color.B + delta);

                    if (color.R + delta > 255) r = byte.MaxValue;
                    else if (color.R + delta < 0) r = byte.MinValue;
                    if (color.G + delta > 255) g = byte.MaxValue;
                    else if (color.G + delta < 0) g = byte.MinValue;
                    if (color.B + delta > 255) b = byte.MaxValue;
                    else if (color.B + delta < 0) b = byte.MinValue;

                    color = Color.FromArgb(r, g, b);

                    _secondWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
        }

        private void ContrastButton_Click(object sender, EventArgs e)
        {
            var minR = byte.MaxValue;
            var minG = byte.MaxValue;
            var minB = byte.MaxValue;

            var maxR = byte.MinValue;
            var maxG = byte.MinValue;
            var maxB = byte.MinValue;

            _firstWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var color = _firstWorkImage.GetPixel(i, j);

                    if (color.R < minR) minR = color.R;
                    if (color.B < minB) minB = color.B;
                    if (color.G < minG) minG = color.G;

                    if (color.R > maxR) maxR = color.R;
                    if (color.B > maxB) maxB = color.B;
                    if (color.G > maxG) maxG = color.G;
                }
            }
            _firstWorkImage.Unlock();

            var delta = ContrastTrackBar.Value;

            int aR = (minR - delta);
            int bR = (maxR + delta);

            int aB = (minB - delta);
            int bB = (maxB + delta);

            int aG = (minG - delta);
            int bG = (maxG + delta);

            _secondWorkImage.Lock();
            _firstWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var color = _firstWorkImage.GetPixel(i, j);

                    var r = (((bR - aR) * (color.R - minR)) / (maxR - minR) + aR);
                    var b = (((bB - aB) * (color.B - minB)) / (maxB - minB) + aB);
                    var g = (((bG - aG) * (color.G - minG)) / (maxG - minG) + aG);

                    if (r > 255) r = byte.MaxValue;
                    else if (r < 0) r = byte.MinValue;
                    if (g > 255) g = byte.MaxValue;
                    else if (g < 0) g = byte.MinValue;
                    if (b > 255) b = byte.MaxValue;
                    else if (b < 0) b = byte.MinValue;

                    color = Color.FromArgb(r, g, b);

                    _secondWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
        }

        readonly int[] _histograma = new int[256];
        readonly int[] _histogramaCumulativa = new int[256];
        readonly int[] _transf = new int[256];

        private void HistogramaButton_Click(object sender, EventArgs e)
        {
            Color pixelColor;

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    int intensitate = pixelColor.R;
                    _histograma[intensitate]++;
                }
            }

            for (int i = 1; i <= 255; i++)
            {
                _histogramaCumulativa[i] = _histogramaCumulativa[i - 1] + _histograma[i];
            }
            for (int i = 0; i <= 255; i++)
            {
                _transf[i] = (_histogramaCumulativa[i] * 255) / (_firstWorkImage.Width * _firstWorkImage.Height);
            }
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    int intensitate = pixelColor.R;

                    pixelColor = Color.FromArgb(_transf[intensitate], _transf[intensitate], _transf[intensitate]);
                    _secondWorkImage.SetPixel(i, j, pixelColor);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            var c = _histograma;
            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
        }

        private void ScalingButton_Click(object sender, EventArgs e)
        {
            Color pixelColor;

            double[,] matriceScalare;

            matriceScalare = new double[2, 2];
            matriceScalare[0, 0] = 0.5;
            matriceScalare[0, 1] = 0;
            matriceScalare[1, 0] = 0;
            matriceScalare[1, 1] = 0.5;


            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                    int x = 0;
                    int y = 0;
                    x = (int)(matriceScalare[0, 0] * i + matriceScalare[0, 1] * j);
                    y = (int)(matriceScalare[1, 0] * i + matriceScalare[1, 1] * j);
                    if (x >= 0 && y >= 0)
                    {
                        _secondWorkImage.SetPixel(x, y, pixelColor);
                    }
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private void RotateButton_Click(object sender, EventArgs e)
        {
            Color pixelColor;

            double unghi = 37;
            double[,] matriceRotatie;
            bool[,] verificare;
            matriceRotatie = new double[2, 2];
            matriceRotatie[0, 0] = Math.Cos(ToRadians(unghi));
            matriceRotatie[0, 1] = Math.Sin(ToRadians(unghi));
            matriceRotatie[1, 0] = Math.Sin(ToRadians(unghi));
            matriceRotatie[1, 1] = Math.Cos(ToRadians(unghi));
            verificare = new bool[_firstWorkImage.Width, _firstWorkImage.Height];

            var x0 = _firstWorkImage.Width / 2;
            var y0 = _firstWorkImage.Height / 2;

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                }
            }

            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    //_secondWorkImage.SetPixel(i, j, Color.Black);
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    int x = 0;
                    int y = 0;
                    x = (int)(matriceRotatie[0, 0] * (i - x0) - matriceRotatie[0, 1] * (j - y0) + x0);
                    y = (int)(matriceRotatie[1, 0] * (i - x0) + matriceRotatie[1, 1] * (j - y0) + y0);
                    if (x >= 0 && y >= 0)
                    {
                        if (x < _firstWorkImage.Width && y < _firstWorkImage.Height)
                        {
                            _secondWorkImage.SetPixel(x, y, pixelColor);
                            verificare[x, y] = true;
                        }
                    }
                }
            }

            
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    if (verificare[i, j] == false && j > 0)
                    {
                       
                        if(  verificare[i, j - 1] == true)
                        {
                            pixelColor = _secondWorkImage.GetPixel(i, j - 1);
                            _secondWorkImage.SetPixel(i, j, pixelColor);
                        }
                    }
                }
            }
            
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
            _secondImage.Save("image.png");

        }
        private double ToRadians(double angle)
        {
            // Angle in 10th of a degree
            return (angle * Math.PI) / 180;
        }

        double unghiAxa = Math.PI / 2;
        int x0 = 0, y0 = 100;

        private void ReflectionButton_Click(object sender, EventArgs e)
        {
            Reflexie();
        }

        public void Reflexie()
        {
            Color pixelColor = default(Color);
            double delta = 0;
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                }
            }
            x0 = _firstWorkImage.Width / 2;
            y0 = _firstWorkImage.Height / 2;
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    int x = i;
                    int y = -j + 2 * y0;
                    delta = (i - x0) * Math.Sin(unghiAxa) - (j - y0) * Math.Cos(unghiAxa);
                    x = (int)(i - 2 * delta * Math.Sin(unghiAxa));
                    y = (int)(j + 2 * delta * Math.Cos(unghiAxa));
                    if (x >= 0 && y >= 0 && x < _firstWorkImage.Width && y < _firstWorkImage.Height)
                    {
                        _secondWorkImage.SetPixel(x, y, pixelColor);
                    }
                }
            }


            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
        }

        int translatieX = 50, translatieY = 0;

        private void TranslationButton_Click(object sender, EventArgs e)
        {
            Translatie();
        }

        private void Translatie()
        {
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                }
            }
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
                {
                    var pixelColor = _firstWorkImage.GetPixel(i, j);
                    int x = 0;
                    int y = 0;
                    x = i + translatieX;
                    y = j + translatieY;
                    if (x >= 0 && y >= 0 && x < _firstWorkImage.Width && y < _firstWorkImage.Height)
                    {
                        _secondWorkImage.SetPixel(x, y, pixelColor);
                    }
                }
            }


            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

    }
}