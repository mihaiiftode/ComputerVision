using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerVision
{
    public class FastImage
    {
        public int Height = 0;
        public int Width = 0;
        private Bitmap _image = null;
        private Rectangle _rectangle;
        private BitmapData _bitmapData = null;
        private Color _color;
        private Point _size;
        private int _currentBitmapWidth = 0;

        struct PixelData
        {
            public byte Red, Green, Blue;
        }

        public FastImage(Bitmap bitmap)
        {
            _image = bitmap;
            Width = _image.Width;
            Height = _image.Height;
            _size = new Point(_image.Size);
            _currentBitmapWidth = _size.X;
        }

        public void Lock()
        {
            // Rectangle For Locking The Bitmap In Memory
            _rectangle = new Rectangle(0, 0, _image.Width, _image.Height);
            // Get The Bitmap's Pixel Data From The Locked Bitmap
            _bitmapData = _image.LockBits(_rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        }

        public void Unlock()
        {
            _image.UnlockBits(_bitmapData);
        }

        public Color GetPixel(int col, int row)
        {
            unsafe
            {
                var pixelBase = (PixelData*)_bitmapData.Scan0;
                var pixel = pixelBase + row * _currentBitmapWidth + col;
                _color = Color.FromArgb(pixel->Red, pixel->Green, pixel->Blue);
            }
            return _color;
        }

        public void SetPixel(int col, int row, Color c)
        {
            unsafe
            {
                var pixelBase = (PixelData*)_bitmapData.Scan0;
                var pixel = pixelBase + row * _currentBitmapWidth + col;
                pixel->Red = c.R;
                pixel->Green = c.G;
                pixel->Blue = c.B;
            }
        }

        public Bitmap GetBitMap()
        {
            return _image;
        }

    }
}
