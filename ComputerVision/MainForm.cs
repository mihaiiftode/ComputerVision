using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Linq;

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

            var aR = minR - delta;
            var bR = maxR + delta;

            var aB = minB - delta;
            var bB = maxB + delta;

            var aG = minG - delta;
            var bG = maxG + delta;

            _secondWorkImage.Lock();
            _firstWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var color = _firstWorkImage.GetPixel(i, j);

                    var r = (bR - aR) * (color.R - minR) / (maxR - minR) + aR;
                    var b = (bB - aB) * (color.B - minB) / (maxB - minB) + aB;
                    var g = (bG - aG) * (color.G - minG) / (maxG - minG) + aG;

                    ColorBounds(ref r, ref g, ref b);

                    color = Color.FromArgb(r, g, b);

                    _secondWorkImage.SetPixel(i, j, color);
                }
            }
            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
        }

        private static void ColorBounds(ref int r, ref int g, ref int b)
        {
            if (r > 255) r = byte.MaxValue;
            else if (r < 0) r = byte.MinValue;
            if (g > 255) g = byte.MaxValue;
            else if (g < 0) g = byte.MinValue;
            if (b > 255) b = byte.MaxValue;
            else if (b < 0) b = byte.MinValue;
        }

        readonly int[] _histograma = new int[256];
        readonly int[] _histogramaCumulativa = new int[256];
        readonly int[] _transf = new int[256];

        private void HistogramaButton_Click(object sender, EventArgs e)
        {
            Color pixelColor;

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    int intensitate = pixelColor.R;
                    _histograma[intensitate]++;
                }
            }

            for (var i = 1; i <= 255; i++)
            {
                _histogramaCumulativa[i] = _histogramaCumulativa[i - 1] + _histograma[i];
            }
            for (var i = 0; i <= 255; i++)
            {
                _transf[i] = _histogramaCumulativa[i] * 255 / (_firstWorkImage.Width * _firstWorkImage.Height);
            }
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
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
            matriceScalare[0, 0] = 2;
            matriceScalare[0, 1] = 2;
            matriceScalare[1, 0] = 2;
            matriceScalare[1, 1] = 2;

            int width = _firstWorkImage.Width * 2, height = _secondWorkImage.Height * 2;
            _secondWorkImage = new FastImage(new Bitmap(width, height));

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    _secondWorkImage.SetPixel(i * 2, j * 2, pixelColor);
                    _secondWorkImage.SetPixel(i * 2 + 1, j * 2, pixelColor);
                    _secondWorkImage.SetPixel(i * 2, j * 2 + 1, pixelColor);
                    _secondWorkImage.SetPixel(i * 2 + 1, j * 2 + 1, pixelColor);
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
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                }
            }

            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    //_secondWorkImage.SetPixel(i, j, Color.Black);
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    var x = 0;
                    var y = 0;
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


            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    if (verificare[i, j] == false && j > 0)
                    {
                        if (verificare[i, j - 1] == true)
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
            return angle * Math.PI / 180;
        }

        double unghiAxa = Math.PI / 2;
        int x0 = 0, y0 = 100;

        private void ReflectionButton_Click(object sender, EventArgs e)
        {
            Reflexie();
        }

        public void Reflexie()
        {
            var pixelColor = default(Color);
            double delta = 0;
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                }
            }
            x0 = _firstWorkImage.Width / 2;
            y0 = _firstWorkImage.Height / 2;
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    pixelColor = _firstWorkImage.GetPixel(i, j);
                    var x = i;
                    var y = -j + 2 * y0;
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
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    _secondWorkImage.SetPixel(i, j, Color.Black);
                }
            }
            for (var i = 0; i < _firstWorkImage.Width; i++)
            {
                for (var j = 0; j < _firstWorkImage.Height; j++)
                {
                    var pixelColor = _firstWorkImage.GetPixel(i, j);
                    var x = 0;
                    var y = 0;
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

        private void FtjButton_Click(object sender, EventArgs e)
        {
            var n = (int)FtjUpDown.Value;

            var factor = (n + 2) * (n + 2);

            double[,] h =
            {
                {1, n, 1},
                {n, n * n, n},
                {1, n, 1}
            };

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var color = ApplyConvolution(col, row, h, factor);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }


        private Color ApplyConvolution(int col, int row, double[,] h, int factor)
        {
            var redSum = 0d;
            var blueSum = 0d;
            var greenSum = 0d;
            for (int i = 0, indexRow = -1; i < 3; i++, indexRow++)
            {
                for (int j = 0, indexCol = -1; j < 3; j++, indexCol++)
                {
                    redSum += _firstWorkImage.GetPixel(col + indexCol, row + indexRow).R * h[i, j];
                    greenSum += _firstWorkImage.GetPixel(col + indexCol, row + indexRow).G * h[i, j];
                    blueSum += _firstWorkImage.GetPixel(col + indexCol, row + indexRow).B * h[i, j];
                }
            }
            var r = (int)(redSum / factor);
            var g = (int)(greenSum / factor);
            var b = (int)(blueSum / factor);

            ColorBounds(ref r, ref g, ref b);

            return Color.FromArgb(r, g, b);
        }

        private Color ApplyConvolution(int col, int row, int[,] h, int factor)
        {
            var redSum = 0;
            var blueSum = 0;
            var greenSum = 0;
            for (int i = 0, indexRow = -1; i < 3; i++, indexRow++)
            {
                for (int j = 0, indexCol = -1; j < 3; j++, indexCol++)
                {
                    redSum += _firstWorkImage.GetPixel(col + indexCol, row + indexRow).R * h[i, j];
                    greenSum += _firstWorkImage.GetPixel(col + indexCol, row + indexRow).G * h[i, j];
                    blueSum += _firstWorkImage.GetPixel(col + indexCol, row + indexRow).B * h[i, j];
                }
            }
            var r = redSum / factor;
            var g = greenSum / factor;
            var b = blueSum / factor;

            ColorBounds(ref r, ref g, ref b);

            return Color.FromArgb(r, g, b);
        }

        private void OutlierButton_Click(object sender, EventArgs e)
        {
            var epsilon = (int)FtjUpDown.Value;
            var n = 0;
            var factor = 8;

            double[,] h =
            {
                {1, 1, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var color = ApplyConvolution(col, row, h, factor);
                    var originalColor = _firstWorkImage.GetPixel(col, row);
                    var grayScale = (originalColor.R + originalColor.G + originalColor.B) / 3;
                    if (Math.Abs((color.R + color.G + color.B) / 3 - grayScale) > epsilon)
                        _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private void MedianButton_Click(object sender, EventArgs e)
        {
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 0; row < _firstWorkImage.Height; row++)
            {
                for (var col = 2; col < _firstWorkImage.Width - 2; col++)
                {
                    var color = GetMedian(_firstWorkImage, col, row);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private Color GetMedian(FastImage fastImage, int col, int row)
        {
            var a = fastImage.GetPixel(col - 2, row);
            var b = fastImage.GetPixel(col - 1, row);
            var c = fastImage.GetPixel(col, row);
            var d = fastImage.GetPixel(col + 1, row);
            var e = fastImage.GetPixel(col + 2, row);


            var listOfColors = new List<Color>
            {
                GetMinimunColor(a, b, c),
                GetMinimunColor(a, b, d),
                GetMinimunColor(a, b, e),
                GetMinimunColor(a, c, d),
                GetMinimunColor(a, c, e),
                GetMinimunColor(a, d, e),
                GetMinimunColor(b, c, d),
                GetMinimunColor(b, c, e),
                GetMinimunColor(b, d, e),
                GetMinimunColor(c, d, e)
            };
            return GetMaxium(listOfColors);
        }

        private Color GetMaxium(List<Color> listOfColors)
        {
            var maxRed = int.MinValue;
            var maxGreen = int.MinValue;
            var maxBlue = int.MinValue;


            foreach (var color in listOfColors)
            {
                if (color.R > maxRed)
                    maxRed = color.R;
                if (color.B > maxBlue)
                    maxBlue = color.B;
                if (color.G > maxGreen)
                    maxGreen = color.G;
            }

            ColorBounds(ref maxRed, ref maxGreen, ref maxBlue);

            return Color.FromArgb(maxRed, maxGreen, maxBlue);
        }

        private Color GetMinimunColor(Color color1, Color color2, Color color3)
        {
            var minAbcRed = GetMinimun(color1.R, color2.R, color3.R);
            var minAbcGreen = GetMinimun(color1.G, color2.G, color3.G);
            var minAbcBlue = GetMinimun(color1.B, color2.B, color3.B);

            return Color.FromArgb(minAbcRed, minAbcGreen, minAbcBlue);
        }


        private int GetMinimun(byte a, byte b, byte c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        private void FTSButton_Click(object sender, EventArgs e)
        {
            double[,] h =
            {
                {-1, -1, -1},
                {-1, 9, -1},
                {-1, -1, -1}
            };

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var color = ApplyConvolution(col, row, h, 1);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }


        private void UnsharpMaskButton_Click(object sender, EventArgs e)
        {
            var n = 1;
            var c = 0.6d;
            var factor = (n + 2) * (n + 2);

            double[,] h =
            {
                {1, n, 1},
                {n, n * n, n},
                {1, n, 1}
            };

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var originalColor = _firstWorkImage.GetPixel(col, row);
                    var colorFTJ = ApplyConvolution(col, row, h, factor);
                    var red = (c * originalColor.R - (1 - c) * colorFTJ.R) / (2 * c - 1);
                    var green = (c * originalColor.G - (1 - c) * colorFTJ.G) / (2 * c - 1);
                    var blue = (c * originalColor.B - (1 - c) * colorFTJ.B) / (2 * c - 1);

                    var r = (int)red;
                    var g = (int)green;
                    var b = (int)blue;
                    ColorBounds(ref r, ref g, ref b);

                    var color = Color.FromArgb(r, g, b);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private void KirschButton_Click(object sender, EventArgs e)
        {
            var n = 1;
            var c = 0.6d;

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var center = _firstWorkImage.GetPixel(col, row);
                    var left = _firstWorkImage.GetPixel(col - 1, row);
                    var right = _firstWorkImage.GetPixel(col + 1, row);
                    var bottom = _firstWorkImage.GetPixel(col, row + 1);

                    var red = GetKirsch(center.R, bottom.R, right.R, left.R);
                    var green = GetKirsch(center.G, bottom.G, right.G, left.G);
                    var blue = GetKirsch(center.B, bottom.B, right.B, left.B);

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private byte GetKirsch(byte center, byte bottom, byte right, byte left)
        {
            int[,] h1 =
            {
                {1},
                {-1}
            };
            int[] h2 = { 1, -1 };
            int[] h3 = { 1, 0, -1 };

            var h1Sum = center * h1[0, 0] + bottom * h1[1, 0];
            var h2Sum = center * h2[0] + right * h2[1];
            var h3Sum = left * h3[0] + center * h3[1] + right * h3[2];

            var max = Math.Max(h3Sum, Math.Max(h1Sum, h2Sum));

            ColorBounds(ref max, ref max, ref max);

            return (byte)max;
        }


        private void Kirch2Button_Click(object sender, EventArgs e)
        {
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var color = GetKirsch2(col, row);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private Color GetKirsch2(int col, int row)
        {
            double[,] h1 =
            {
                {-1, 0, 1},
                {-1, 0, 1},
                {-1, 0, 1}
            };
            double[,] h2 =
            {
                {1, 1, 1},
                {0, 0, 0},
                {-1, -1, -1}
            };
            double[,] h3 =
            {
                {0, 1, 1},
                {-1, 0, 1},
                {-1, -1, 0}
            };
            double[,] h4 =
            {
                {1, 1, 0},
                {1, 0, -1},
                {0, -1, -1}
            };

            var colors = new List<Color>
            {
                ApplyConvolution(col, row, h1, 1),
                ApplyConvolution(col, row, h2, 1),
                ApplyConvolution(col, row, h3, 1),
                ApplyConvolution(col, row, h4, 1),
            };

            int maxRed = byte.MinValue;
            int maxGreen = byte.MinValue;
            int maxBlue = byte.MinValue;

            foreach (var color in colors)
            {
                if (color.R > maxRed)
                    maxRed = color.R;
                if (color.B > maxBlue)
                    maxBlue = color.B;
                if (color.G > maxGreen)
                    maxGreen = color.G;
            }

            ColorBounds(ref maxRed, ref maxGreen, ref maxBlue);
            return Color.FromArgb(maxRed, maxGreen, maxBlue);
        }

        private void LaplaceButton_Click(object sender, EventArgs e)
        {
            var factor = 1;

            double[,] h =
            {
                {-1, -1, -1},
                {-1, 8, -1},
                {-1, -1, -1}
            };

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var color = ApplyConvolution(col, row, h, factor);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private void RobertsButton_Click(object sender, EventArgs e)
        {
            int[,] p =
            {
                {-1, 0},
                {0, 1}
            };
            int[,] q =
            {
                {0, 1},
                {-1, 0}
            };

            var k = 7;

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var pColor = GetRoberts(2, col, row, p);
                    var qColor = GetRoberts(2, col, row, q);

                    var red = (int)(k * Math.Sqrt(pColor.R * pColor.R + qColor.R * qColor.R));
                    var green = (int)(k * Math.Sqrt(pColor.G * pColor.G + qColor.G * qColor.G));
                    var blue = (int)(k * Math.Sqrt(pColor.B * pColor.B + qColor.B * qColor.B));

                    ColorBounds(ref red, ref green, ref blue);

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private Color GetRoberts(int n, int col, int row, int[,] p)
        {
            int redSum = 0, blueSum = 0, greenSum = 0;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    redSum += _firstWorkImage.GetPixel(col + j, row + i).R * p[i, j];
                    greenSum += _firstWorkImage.GetPixel(col + j, row + i).G * p[i, j];
                    blueSum += _firstWorkImage.GetPixel(col + j, row + i).B * p[i, j];
                }
            }

            ColorBounds(ref redSum, ref blueSum, ref greenSum);

            return Color.FromArgb(redSum, greenSum, blueSum);
        }

        private void PseudoMedianButton_Click(object sender, EventArgs e)
        {

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 0; row < _firstWorkImage.Height; row++)
            {
                for (var col = 2; col < _firstWorkImage.Width - 2; col++)
                {
                    var pseudoMedian = GetPseudoMedian(col, row);
                    _secondWorkImage.SetPixel(col, row, pseudoMedian);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();

        }

        private Color GetPseudoMedian(int col, int row)
        {
            var a = _firstWorkImage.GetPixel(col - 2, row);
            var b = _firstWorkImage.GetPixel(col - 1, row);
            var c = _firstWorkImage.GetPixel(col, row);
            var d = _firstWorkImage.GetPixel(col + 1, row);
            var e = _firstWorkImage.GetPixel(col + 2, row);

            var pseudoMedianColor = GetPseudoMedianColor(a, b, c, d, e);

            return pseudoMedianColor;
        }

        private Color GetPseudoMedianColor(Color a, Color b, Color c, Color d, Color e)
        {

            var maximumColor = GetMaxiMinColor(a, b, c, d, e);
            var halfMaxiMinColor = Color.FromArgb(maximumColor.R / 2, maximumColor.G / 2, maximumColor.B / 2);

            var minimumColor = GetMinMaxColor(a, b, c, d, e);
            var halfMiniMaxColor = Color.FromArgb(minimumColor.R / 2, minimumColor.G / 2, minimumColor.B / 2);

            var pseudoMedianColor = Color.FromArgb(halfMaxiMinColor.R + halfMiniMaxColor.R, halfMaxiMinColor.G + halfMiniMaxColor.G,
                halfMaxiMinColor.B + halfMiniMaxColor.B);

            return pseudoMedianColor;
        }

        private Color GetMinMaxColor(Color a, Color b, Color c, Color d, Color e)
        {
            var listOfMaxColors = new List<Color>()
            {
                GetMaximum(a, b, c),
                GetMaximum(b, c, d),
                GetMaximum(c, d, e)
            };

            var minimumColor = GetMinimum(listOfMaxColors);

            return minimumColor;
        }

        private Color GetMaximum(Color color1, Color color2, Color color3)
        {
            var maxAbcRed = MaxOf3(color1.R, color2.R, color3.R);
            var maxAbcGreen = MaxOf3(color1.G, color2.G, color3.G);
            var maxAbcBlue = MaxOf3(color1.B, color2.B, color3.B);

            return Color.FromArgb(maxAbcRed, maxAbcGreen, maxAbcBlue);

        }

        private int MaxOf3(byte a, byte b, byte c)
        {
            return Math.Max(Math.Max(a, b), c);
        }
        private Color GetMaxiMinColor(Color a, Color b, Color c, Color d, Color e)
        {
            var listOfMinColors = new List<Color>
            {
                GetMinimunColor(a, b, c),
                GetMinimunColor(b, c, d),
                GetMinimunColor(c, d, e)
            };
            var maximumColor = GetMaxium(listOfMinColors);
            return maximumColor;
        }

        private Color GetMinimum(List<Color> listOfColors)
        {
            var minRed = byte.MaxValue;
            var minGreen = byte.MaxValue;
            var minBlue = byte.MaxValue;

            foreach (var color in listOfColors)
            {
                if (color.R < minRed)
                    minRed = color.R;
                if (color.B < minBlue)
                    minBlue = color.B;
                if (color.G < minGreen)
                    minGreen = color.G;
            }

            return Color.FromArgb(minRed, minGreen, minBlue);
        }

        private void SortIntensityButton_Click(object sender, EventArgs e)
        {


            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var color = GetValueAfterSort(col, row);
                    _secondWorkImage.SetPixel(col, row, color);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private Color GetValueAfterSort(int col, int row)
        {
            List<byte> listOfRedColors = new List<byte>(),
               listOfBlueColors = new List<byte>(),
               listOfGreenColors = new List<byte>();
            var n = 3;

            for (int i = 0, indexRow = 1; i < n; i++, indexRow--)
            {
                for (int j = 0, indexCol = 1; j < n; j++, indexCol--)
                {
                    listOfRedColors.Add(_firstWorkImage.GetPixel(col - indexCol, row - indexRow).R);
                    listOfBlueColors.Add(_firstWorkImage.GetPixel(col - indexCol, row - indexRow).B);
                    listOfGreenColors.Add(_firstWorkImage.GetPixel(col - indexCol, row - indexRow).G);
                }
            }

            listOfRedColors.Sort();
            listOfBlueColors.Sort();
            listOfGreenColors.Sort();

            return Color.FromArgb(listOfRedColors[4], listOfGreenColors[4], listOfBlueColors[4]);
        }

        private void FreiChenButton_Click(object sender, EventArgs e)
        {
            double[,] f1 =
            {
                {1, Math.Sqrt(2), 1},
                {0, 0, 0},
                {-1, -Math.Sqrt(2), -1}
            };
            double[,] f2 =
            {
                {1, 0, -1},
                {Math.Sqrt(2), 0, -Math.Sqrt(2)},
                {1, 0, -1}
            };
            double[,] f3 =
            {
                {0, -1, Math.Sqrt(2)},
                {1, 0, -1},
                {-Math.Sqrt(2), 1, 0}
            };
            double[,] f4 =
            {
                {Math.Sqrt(2), -1, 0},
                {-1, 0, 1},
                {0, 1, -Math.Sqrt(2)}
            };
            double[,] f5 =
            {
                {0, 1, 0},
                {-1, 0, -1},
                {0, 1, 0}
            };
            double[,] f6 =
            {
                {-1, 0, 1},
                {0, 0, 0},
                {1, 0, -1}
            };
            double[,] f7 =
            {
                {1, -2, 1},
                {-2, 4, -2},
                {1, -2, 1}
            };
            double[,] f8 =
            {
                {-2, 1, -2},
                {1, 4, 1},
                {-2, 1, -2}
            };
            double[,] f9 =
            {
                {1/9, 1/9, 1/9},
                {1/9, 1/9, 1/9},
                {1/9, 1/9, 1/9}
            };
            int sum4Red = 0, sum4Green = 0, sum4Blue = 0;
            int sum4To9Red = 0, sum4To9Green = 0, sum4To9Blue = 0;


            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var F1 = ApplyConvolution(col, row, f1, 1);
                    var F2 = ApplyConvolution(col, row, f2, 1);
                    var F3 = ApplyConvolution(col, row, f3, 1);
                    var F4 = ApplyConvolution(col, row, f4, 1);
                    var F5 = ApplyConvolution(col, row, f5, 1);
                    var F6 = ApplyConvolution(col, row, f6, 1);
                    var F7 = ApplyConvolution(col, row, f7, 1);
                    var F8 = ApplyConvolution(col, row, f8, 1);
                    var F9 = ApplyConvolution(col, row, f9, 1);


                    sum4Red = F1.R * F1.R + F2.R * F2.R + F3.R * F3.R + F4.R * F4.R;
                    sum4Green = F1.G * F1.G + F2.G * F2.G + F3.G * F3.G + F4.G * F4.G;
                    sum4Blue = F1.B * F1.B + F2.B * F2.B + F3.B * F3.B + F4.B * F4.B;

                    sum4To9Red = sum4Red + F5.R * F5.R + F6.R * F6.R + F7.R * F7.R + F8.R * F8.R + F9.R * F9.R;
                    sum4To9Green = sum4Green + F5.G * F5.G + F6.G * F6.G + F7.G * F7.G + F8.G * F8.G + F9.G * F9.G;
                    sum4To9Blue = sum4Blue + F5.B * F5.B + F6.B * F6.B + F7.B * F7.B + F8.B * F8.B + F9.B * F9.B;


                    var red = 0;
                    var green = 0;
                    var blue = 0;

                    if (sum4To9Red != 0 && sum4To9Green != 0 && sum4To9Blue != 0)
                    {
                        red = (int)(Math.Sqrt(sum4Red / (double)sum4To9Red) * 255.0);
                        green = (int)(Math.Sqrt(sum4Green / (double)sum4To9Green) * 255.0);
                        blue = (int)(Math.Sqrt(sum4Blue / (double)sum4To9Blue) * 255.0);

                        ColorBounds(ref red, ref green, ref blue);
                    }

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();

        }

        private void PrewittButton_Click(object sender, EventArgs e)
        {
            double[,] p =
            {
                {-1, -1, -1},
                {0, 0, 0},
                {1, 1, 1}
            };
            double[,] q =
            {
                {-1, 0, 1},
                {-1, 0, 1},
                {-1, 0, 1}
            };


            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var pColor = ApplyConvolution(col, row, p, 1);
                    var qColor = ApplyConvolution(col, row, q, 1);

                    var red = (int)Math.Sqrt(pColor.R * pColor.R + qColor.R * qColor.R);
                    var green = (int)Math.Sqrt(pColor.G * pColor.G + qColor.G * qColor.G);
                    var blue = (int)Math.Sqrt(pColor.B * pColor.B + qColor.B * qColor.B);

                    ColorBounds(ref red, ref green, ref blue);

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }

        private void SobelButton_Click(object sender, EventArgs e)
        {
            double[,] p =
         {
                {-1, -2, -1},
                {0, 0, 0},
                {1, 2, 1}
            };
            double[,] q =
            {
                {-1, 0, 1},
                {-2, 0, 2},
                {-1, 0, 1}
            };


            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {

                    var pColor = ApplyConvolution(col, row, p, 1);
                    var qColor = ApplyConvolution(col, row, q, 1);

                    var red = (int)Math.Sqrt(pColor.R * pColor.R + qColor.R * qColor.R);
                    var green = (int)Math.Sqrt(pColor.G * pColor.G + qColor.G * qColor.G);
                    var blue = (int)Math.Sqrt(pColor.B * pColor.B + qColor.B * qColor.B);
                    ColorBounds(ref red, ref green, ref blue);

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }


        private void GaborButton_Click(object sender, EventArgs e)
        {
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            double[,] p =
            {
                {1, 1, 1},
                {0, 0, 0},
                {-1, -1, -1}
            };
            double[,] q =
            {
                {-1, 0, 1},
                {-1, 0, 1},
                {-1, 0, 1}
            };

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    var pColor = ApplyConvolution(col, row, p, 1);
                    var qColor = ApplyConvolution(col, row, q, 1);

                    var uRed = GetU(pColor.R, qColor.R);
                    var uGreen = GetU(pColor.G, qColor.G);
                    var uBlue = GetU(pColor.B, qColor.B);

                    double sumRed = 0, sumBlue = 0, sumGreen = 0;
                    var n = 3;
                    //Color c = _firstWorkImage.GetPixel(col, row);
                    for (var roww = row - 1; roww <= row + 1; roww++)
                    {
                        for (var coll = col - 1; coll <= col + 1; coll++)
                        {
                            var redScale = ApplyScaleFormula(roww - row + 1, coll - col + 1, uRed);
                            var greenScale = ApplyScaleFormula(roww - row + 1, coll - col + 1, uGreen);
                            var blueScale = ApplyScaleFormula(roww - row + 1, coll - col + 1, uBlue);

                            var c = _firstWorkImage.GetPixel(col, row);

                            sumRed += redScale * c.R + 10;
                            sumGreen += greenScale * c.G + 10;
                            sumBlue += blueScale * c.B + 10;
                        }
                    }
                    var red = (int)sumRed;
                    var green = (int)sumGreen;
                    var blue = (int)sumBlue;

                    ColorBounds(ref red, ref green, ref blue);

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();
            DestinationPanel.SizeMode = PictureBoxSizeMode.StretchImage;


            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();

        }

        private double ApplyScaleFormula(int row, int col, double u)
        {
            var sigma = 0.66;
            var omega = 1.5;

            return Math.Exp(-((row * row + col * col) / (2 * sigma * sigma))) * Math.Sin(omega * (row * Math.Cos(u) + col * Math.Sin(u)));
        }

        private double GetU(int pColor, int qColor)
        {
            double u = 0;

            if (qColor == 0)
            {
                u = -Math.PI / 2.0;

                if (pColor >= 0)
                    u = Math.PI / 2.0;
            }
            else
            {
                u = Math.Atan(pColor / (double)qColor);

                if (qColor < 0)
                    u += Math.PI;
            }

            u += Math.PI / 2.0;

            return u;
        }

        private void RegionGrowingButton_Click(object sender, EventArgs e)
        {
            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 0; row < _firstWorkImage.Height - 1; row++)
                for (var col = 0; col < _firstWorkImage.Width - 1; col++)
                    _secondWorkImage.SetPixel(col, row, Color.White);

            var x = int.Parse(this.x.Text);
            var y = int.Parse(this.y.Text);

            var listOfPoints = new List<Point>
            {
                new Point(x, y)
            };

            var pixel = _firstWorkImage.GetPixel(x, y);
            var average = (pixel.R + pixel.B + pixel.G) / 3;
            _secondWorkImage.SetPixel(x, y, pixel);
            var count = 1;
            var matrix = new bool[_firstWorkImage.Height, _firstWorkImage.Width];
            for (var row = 0; row < matrix.GetLength(0); row++)
                for (var col = 0; col < matrix.GetLength(1); col++)
                    matrix[row, col] = false;

            matrix[y, x] = true;

            while (listOfPoints.Count != 0)
            {
                var currentPoint = listOfPoints[0];

                //up
                var t = int.Parse(value.Text);

                if (IsWithinInterval(_firstWorkImage, currentPoint.X, currentPoint.Y - 1,
                    _firstWorkImage.Width, _firstWorkImage.Height, average, matrix, t, count))
                {

                    average = AddPointToList(currentPoint.X, currentPoint.Y - 1, average, listOfPoints);
                    count++;
                    matrix[currentPoint.Y - 1, currentPoint.X] = true;
                    _secondWorkImage.SetPixel(currentPoint.X, currentPoint.Y - 1, _firstWorkImage.GetPixel(currentPoint.X, currentPoint.Y - 1));
                }

                //down
                if (IsWithinInterval(_firstWorkImage, currentPoint.X, currentPoint.Y + 1,
                    _firstWorkImage.Width, _firstWorkImage.Height, average, matrix, t, count))
                {
                    average = AddPointToList(currentPoint.X, currentPoint.Y + 1, average, listOfPoints);
                    count++;
                    matrix[currentPoint.Y + 1, currentPoint.X] = true;
                    _secondWorkImage.SetPixel(currentPoint.X, currentPoint.Y + 1, _firstWorkImage.GetPixel(currentPoint.X, currentPoint.Y + 1));
                }

                //left
                if (IsWithinInterval(_firstWorkImage, currentPoint.X - 1, currentPoint.Y,
                    _firstWorkImage.Width, _firstWorkImage.Height, average, matrix, t, count))
                {
                    average = AddPointToList(currentPoint.X - 1, currentPoint.Y, average, listOfPoints);
                    count++;
                    matrix[currentPoint.Y, currentPoint.X - 1] = true;
                    _secondWorkImage.SetPixel(currentPoint.X - 1, currentPoint.Y, _firstWorkImage.GetPixel(currentPoint.X - 1, currentPoint.Y));
                }

                //right
                if (IsWithinInterval(_firstWorkImage, currentPoint.X + 1, currentPoint.Y,
                    _firstWorkImage.Width, _firstWorkImage.Height, average, matrix, t, count))
                {
                    average = AddPointToList(currentPoint.X + 1, currentPoint.Y, average, listOfPoints);
                    count++;
                    matrix[currentPoint.Y, currentPoint.X + 1] = true;
                    _secondWorkImage.SetPixel(currentPoint.X + 1, currentPoint.Y, _firstWorkImage.GetPixel(currentPoint.X + 1, currentPoint.Y));
                }

                listOfPoints.RemoveAt(0);
            }


            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _secondWorkImage.Unlock();
            _firstWorkImage.Unlock();
        }



        private int AddPointToList(int x, int y, int average, List<Point> listOfPoints)
        {
            var color = _firstWorkImage.GetPixel(x, y);
            var avg = (color.R + color.G + color.B) / 3;
            average += avg;
            listOfPoints.Add(new Point(x, y));

            return average;
        }

        private bool IsWithinInterval(FastImage workImage, int x, int y, int width, int height, int average, bool[,] matrix, int t, int ct)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                if (matrix[y, x])
                    return false;

                var pixel = workImage.GetPixel(x, y);
                var avg = (pixel.R + pixel.G + pixel.B) / 3;

                if (avg < average / ct - t || avg > average / ct + t)
                    return false;

                return true;
            }

            return false;
        }

        private void SourcePanel_Click(object sender, EventArgs e)
        {
            _point = SourcePanel.TranslatePointToImageCoordinates(PointToClient(Cursor.Position));
            double ratioWidth = (1.0 * SourcePanel.BackgroundImage.Width) / SourcePanel.Width;
            double ratioHeight = (1.0 * SourcePanel.BackgroundImage.Height) / SourcePanel.Height;
            //	Scale the points by our ratio
            double newX = SourcePanel.PointToClient(Cursor.Position).X;
            double newY = SourcePanel.PointToClient(Cursor.Position).Y;
            newX *= ratioWidth;
            newY *= ratioHeight;
            x.Text = Math.Truncate(newX).ToString();
            y.Text = Math.Truncate(newY).ToString();
        }

        private void CorelatieButton_Click(object sender, EventArgs e)
        {
            FastImage template = new FastImage(new Bitmap(
                    @"C:\Users\Mihai\Desktop\#forMihai\TemplateTiger.bmp"));

            _firstWorkImage.Lock();
            template.Lock();

            int sumRed = 0, sumGreen = 0, sumBlue = 0,
                sqrtSumRed = 0, sqrtSumGreen = 0, sqrtSumBlue = 0;

            for (int row = 0; row < template.Height - 1; row++)
            {
                for (int col = 0; col < template.Width - 1; col++)
                {
                    Color color = template.GetPixel(col, row);
                    sumRed += color.R;
                    sumGreen += color.G;
                    sumBlue += color.B;

                    sqrtSumRed += color.R * color.R;
                    sqrtSumGreen += color.G * color.G;
                    sqrtSumBlue += color.B * color.B;
                }
            }

            int meanTeamplateRed, meanTeamplateGreen, meanTeamplateBlue;
            int templateSize = template.Height * template.Width;
            meanTeamplateRed = sumRed / templateSize;
            meanTeamplateGreen = sumGreen / templateSize;
            meanTeamplateBlue = sumBlue / templateSize;

            int var2TemplateRed, var2TemplateGreen, var2TemplateBlue;

            var2TemplateRed = (sqrtSumRed - (sumRed * sumRed)) / templateSize;
            var2TemplateGreen = (sqrtSumGreen - (sumGreen * sumGreen)) / templateSize;
            var2TemplateBlue = (sqrtSumBlue - (sumBlue * sumBlue)) / templateSize;

            int correlationCoefficinet = int.MinValue;
            int x = 0, y = 0;

            for (int row = 0; row < _firstWorkImage.Height - template.Height; row++)
            {
                for (int col = 0; col < _firstWorkImage.Width - template.Width; col++)
                {
                    sumRed = 0;
                    sumGreen = 0;
                    sumBlue = 0;

                    sqrtSumRed = 0;
                    sqrtSumGreen = 0;
                    sqrtSumBlue = 0;

                    int prodSumRed = 0, prodSumGreen = 0, prodSumBlue = 0;
                    Debug.WriteLine(row + "  " + col);
                    for (int roww = 0; roww < template.Height - 1; roww++)
                    {
                        for (int coll = 0; coll < template.Width - 1; coll++)
                        {
                            Color color = _firstWorkImage.GetPixel(col + coll, row + roww);
                            sumRed += color.R;
                            sumGreen += color.G;
                            sumBlue += color.B;

                            sqrtSumRed += color.R * color.R;
                            sqrtSumGreen += color.G * color.G;
                            sqrtSumBlue += color.B * color.B;

                            Color pixel = template.GetPixel(coll, roww);
                            prodSumRed += color.R * pixel.R;
                            prodSumGreen += color.G * pixel.G;
                            prodSumBlue += color.B * pixel.B;
                        }
                    }

                    int var2ImageRed = (sqrtSumRed - (sumRed * sumRed)) / templateSize;
                    int var2ImageGreen = (sqrtSumGreen - (sumGreen * sumGreen)) / templateSize;
                    int var2ImageBlue = (sqrtSumBlue - (sumBlue * sumBlue)) / templateSize;

                    int meanProdSum = (prodSumRed + prodSumGreen + prodSumBlue) / 3;
                    int meanTemplate = (meanTeamplateRed + +meanTeamplateGreen + meanTeamplateBlue) / 3;
                    int meanSum = (sumRed + sumGreen + sumBlue) / 3;
                    int meanVar2Template = (var2TemplateRed + var2ImageGreen + var2ImageBlue) / 3;
                    int meanVar2Image = (var2ImageRed + var2ImageBlue + var2ImageBlue) / 3;

                    int coefValue = (int)((meanProdSum - (meanTemplate * meanSum)) / Math.Sqrt(meanVar2Image * meanVar2Template));

                    if (coefValue > correlationCoefficinet)
                    {
                        correlationCoefficinet = coefValue;
                        y = row;
                        x = col;
                    }

                }
            }

            for (int row = y; row < y + template.Height; row++)
            {
                for (int col = x; col < x + template.Width; col++)
                {
                    _firstWorkImage.SetPixel(col, row, Color.Goldenrod);
                }
            }

            SourcePanel.BackgroundImage = null;
            SourcePanel.BackgroundImage = _firstWorkImage.GetBitMap();

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = template.GetBitMap();

            template.Unlock();
            _firstWorkImage.Unlock();
        }

        string _destinationFileName;
        private FastImage _destinationImage;

        private void LoadBlockMatching_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            _destinationFileName = openFileDialog.FileName;
            DestinationPanel.BackgroundImage = new Bitmap(_destinationFileName);
            _destinationImage = new FastImage(new Bitmap(_destinationFileName));
        }

        private void BlockMatchingButton_Click(object sender, EventArgs e)
        {
            FastImage bitmap = new FastImage(new Bitmap(_destinationFileName));

            _destinationImage.Lock();
            _firstWorkImage.Lock();
            bitmap.Lock();

            int n = 1;

            for (int row = n / 2; row < _firstWorkImage.Height - n / 2; row++)
            {
                for (int col = n / 2; col < _firstWorkImage.Width - n / 2; col++)
                {
                    Debug.WriteLine(col + " " + row);
                    FindSimilarBlock(col, row, bitmap);
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = bitmap.GetBitMap();

            bitmap.Unlock();
            _firstWorkImage.Unlock();
            _destinationImage.Unlock();
        }

        private void FindSimilarBlock(int col, int row, FastImage bitmap)
        {
            int n = 1;
            int radius = n* 20;
            int rowUpperLimit = (row + radius) < _firstWorkImage.Height ? row + radius : _firstWorkImage.Height;
            int colUpperLimit = (col + radius) < _firstWorkImage.Width ? col + radius : _firstWorkImage.Width;

            int rowLowerLimit = (row - radius) < 0 ? 0 : row - radius;

            for (int roww = rowLowerLimit; roww < rowUpperLimit; roww++)
            {
                int colLowerLimit = (col - radius) < 0 ? 0 : col - radius;

                for (int coll = colLowerLimit; coll < colUpperLimit ; coll++)
                {
                    var sum = 0;

                    for (int r = roww; r <= roww + n; r++)
                    {
                        for (int c = coll; c < coll + n; c++)
                        {
                            Color pixelToFind = _firstWorkImage.GetPixel(c, r);
                            int avgPixelToFind = (pixelToFind.R + pixelToFind.G + pixelToFind.B) / 3;
                            Color pixelFromSecondFrame = _destinationImage.GetPixel(c, r);
                            int avgPixelFromSecondFram = (pixelFromSecondFrame.R + pixelFromSecondFrame.G +
                                                          pixelFromSecondFrame.B) / 3;
                            sum += (int)Math.Pow((avgPixelToFind - avgPixelFromSecondFram), 2);
                        }
                    }


                    if (sum == 0)
                    {
                        //deplasare pe diagonala
                        {
                            for (int i = row; i <= roww; i++)
                                for (int j = col; j < coll; j++)
                                    bitmap.SetPixel(j, i, Color.Red);
                        }
                    }
                }
            }
        }

        private Point _point;
        private void DestinationPanel_Click(object sender, EventArgs e)
        {

        }
    }
}