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

                    var average = (byte) ((r + g + b) / 3);

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
                    var r = (byte) (byte.MaxValue - color.R);
                    var g = (byte) (byte.MaxValue - color.G);
                    var b = (byte) (byte.MaxValue - color.B);

                    var average = (byte) ((r + g + b) / 3);

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

            int aR = minR - delta;
            int bR = maxR + delta;

            int aB = minB - delta;
            int bB = maxB + delta;

            int aG = minG - delta;
            int bG = maxG + delta;

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
                _transf[i] = _histogramaCumulativa[i] * 255 / (_firstWorkImage.Width * _firstWorkImage.Height);
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
            matriceScalare[0, 0] = 2;
            matriceScalare[0, 1] = 2;
            matriceScalare[1, 0] = 2;
            matriceScalare[1, 1] = 2;

            int width = _firstWorkImage.Width * 2, height = _secondWorkImage.Height * 2;
            _secondWorkImage = new FastImage(new Bitmap(width, height));

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();
            for (int i = 0; i < _firstWorkImage.Width; i++)
            {
                for (int j = 0; j < _firstWorkImage.Height; j++)
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
                    x = (int) (matriceRotatie[0, 0] * (i - x0) - matriceRotatie[0, 1] * (j - y0) + x0);
                    y = (int) (matriceRotatie[1, 0] * (i - x0) + matriceRotatie[1, 1] * (j - y0) + y0);
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
                    x = (int) (i - 2 * delta * Math.Sin(unghiAxa));
                    y = (int) (j + 2 * delta * Math.Cos(unghiAxa));
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

        private void FtjButton_Click(object sender, EventArgs e)
        {
            var n = (int) FtjUpDown.Value;

            var factor = (n + 2) * (n + 2);

            int[,] h =
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
            var epsilon = (int) FtjUpDown.Value;
            var n = 0;
            var factor = 8;

            int[,] h =
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

            for (int row = 0; row < _firstWorkImage.Height; row++)
            {
                for (int col = 2; col < _firstWorkImage.Width - 2; col++)
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
            int minAbcRed = GetMinimun(color1.R, color2.R, color3.R);
            int minAbcGreen = GetMinimun(color1.G, color2.G, color3.G);
            int minAbcBlue = GetMinimun(color1.B, color2.B, color3.B);

            return Color.FromArgb(minAbcRed, minAbcGreen, minAbcBlue);
        }


        private int GetMinimun(byte a, byte b, byte c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        private void FTSButton_Click(object sender, EventArgs e)
        {
            int[,] h =
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

            int[,] h =
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

                    int r = (int) red;
                    int g = (int) green;
                    int b = (int) blue;
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
                    Color center = _firstWorkImage.GetPixel(col, row);
                    Color left = _firstWorkImage.GetPixel(col - 1, row);
                    Color right = _firstWorkImage.GetPixel(col + 1, row);
                    Color bottom = _firstWorkImage.GetPixel(col, row + 1);

                    byte red = GetKirsch(center.R, bottom.R, right.R, left.R);
                    byte green = GetKirsch(center.G, bottom.G, right.G, left.G);
                    byte blue = GetKirsch(center.B, bottom.B, right.B, left.B);

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
            int[] h2 = {1, -1};
            int[] h3 = {1, 0, -1};

            int h1Sum = center * h1[0, 0] + bottom * h1[1, 0];
            int h2Sum = center * h2[0] + right * h2[1];
            int h3Sum = left * h3[0] + center * h3[1] + right * h3[2];

            int max = Math.Max(h3Sum, Math.Max(h1Sum, h2Sum));

            ColorBounds(ref max, ref max, ref max);

            return (byte) max;
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
            int[,] h1 =
            {
                {-1, 0, 1},
                {-1, 0, 1},
                {-1, 0, 1}
            };
            int[,] h2 =
            {
                {1, 1, 1},
                {0, 0, 0},
                {-1, -1, -1}
            };
            int[,] h3 =
            {
                {0, 1, 1},
                {-1, 0, 1},
                {-1, -1, 0}
            };
            int[,] h4 =
            {
                {1, 1, 0},
                {1, 0, -1},
                {0, -1, -1}
            };

            List<Color> colors = new List<Color>
            {
                ApplyConvolution(col, row, h1, 1),
                ApplyConvolution(col, row, h2, 1),
                ApplyConvolution(col, row, h3, 1),
                ApplyConvolution(col, row, h4, 1),
            };

            int maxRed = byte.MinValue;
            int maxGreen = byte.MinValue;
            int maxBlue = byte.MinValue;

            foreach (Color color in colors)
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

            int[,] h =
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

            int k = 7;

            _firstWorkImage.Lock();
            _secondWorkImage.Lock();

            for (var row = 1; row < _firstWorkImage.Height - 1; row++)
            {
                for (var col = 1; col < _firstWorkImage.Width - 1; col++)
                {
                    Color pColor = GetRoberts(2, col, row, p);
                    Color qColor = GetRoberts(2, col, row, q);

                    int red = (int) (k * Math.Sqrt(pColor.R * pColor.R + qColor.R * qColor.R));
                    int green = (int) (k * Math.Sqrt(pColor.G * pColor.G + qColor.G * qColor.G));
                    int blue = (int) (k * Math.Sqrt(pColor.B * pColor.B + qColor.B * qColor.B));

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

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    redSum += _firstWorkImage.GetPixel(col + j, row + i).R * p[i, j];
                    greenSum += _firstWorkImage.GetPixel(col + j, row + i).G * p[i, j];
                    blueSum += _firstWorkImage.GetPixel(col + j, row + i).B * p[i, j];
                }
            }

            ColorBounds(ref redSum, ref blueSum, ref greenSum);

            return Color.FromArgb(redSum, greenSum, blueSum);
        }


        private void PrewittButton_Click(object sender, EventArgs e)
        {
            int[,] p =
            {
                {-1, -1, -1},
                {0, 0, 0},
                {1, 1, 1}
            };
            int[,] q =
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
                    Color pColor = ApplyConvolution(col, row, p, 1);
                    Color qColor = ApplyConvolution(col, row, q, 1);

                    int red = (int) Math.Sqrt(pColor.R * pColor.R + qColor.R * qColor.R);
                    int green = (int) Math.Sqrt(pColor.G * pColor.G + qColor.G * qColor.G);
                    int blue = (int) Math.Sqrt(pColor.B * pColor.B + qColor.B * qColor.B);

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
            int[,] p =
         {
                {-1, -2, -1},
                {0, 0, 0},
                {1, 2, 1}
            };
            int[,] q =
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

                    Color pColor = ApplyConvolution(col, row, p, 1);
                    Color qColor = ApplyConvolution(col, row, q, 1);

                    int red = (int)Math.Sqrt(pColor.R * pColor.R + qColor.R * qColor.R);
                    int green = (int)Math.Sqrt(pColor.G * pColor.G + qColor.G * qColor.G);
                    int blue = (int)Math.Sqrt(pColor.B * pColor.B + qColor.B * qColor.B);
                    ColorBounds(ref red, ref green, ref blue);

                    _secondWorkImage.SetPixel(col, row, Color.FromArgb(red, green, blue));
                }
            }

            DestinationPanel.BackgroundImage = null;
            DestinationPanel.BackgroundImage = _secondWorkImage.GetBitMap();

            _firstWorkImage.Unlock();
            _secondWorkImage.Unlock();
        }
    }
}