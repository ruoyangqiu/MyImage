
using MyImage.ImageProcessor;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Net;

namespace MyImage.Service
{
    public class MyImageService
    {

        #region Singleton
        private static volatile MyImageService instance;
        private static readonly object syncRoot = new Object();



        public static MyImageService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MyImageService();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        public static string _myImageUrl;
        private static Image _myimage;
        private static ImageFormat _format;
        private static int OriginalWidth;
        private static int OriginalHeight;
        private IMyImage processor = new MyImageType();

        public bool InitializeImage(string url)
        {
            _myImageUrl = url;
            WebClient wc = new WebClient();
            byte[] bytes;
            try
            {
                 bytes= wc.DownloadData(_myImageUrl);
            } catch(Exception)
            {
                return false;
            }
            MemoryStream ms = new MemoryStream(bytes);
            _myimage = Image.FromStream(ms);
            _format = _myimage.RawFormat;
            OriginalHeight = _myimage.Height;
            OriginalWidth = _myimage.Width;
            //processor.Initialize(_myimage);
            return true;
        }

        public byte[] GetDisplay()
        {
            return ImageToByteArray();
        }

        public byte[] GetRotationByAngle(int angle)
        {
            _myimage = processor.AngleRotation(_myimage, angle, OriginalWidth, OriginalHeight);
            return ImageToByteArray();
        }

        public byte[] GetFlipping(string orientation)
        {
            _myimage = processor.Fliping(_myimage, orientation);
            return ImageToByteArray();
        }

        public byte[] GetGrayScale()
        {
            _myimage = processor.GrayScale(_myimage);
            return ImageToByteArray();
        }

        public byte[] GetResize(int width, int heigth)
        {
            _myimage = processor.Resize(_myimage, width, heigth);
            if(heigth > OriginalHeight)
            {
                OriginalHeight = heigth;
            }
            if (width > OriginalWidth)
            {
                OriginalWidth = width;
            }
            return ImageToByteArray();
        }

        public byte[] GetTumbnail()
        {
            Image thumb = processor.Thumbnail(_myimage);
            MemoryStream ms2 = new MemoryStream();
            ImageFormat format = processor.GetImageFormat(thumb);
            thumb.Save(ms2, _format);
            return ms2.ToArray();
        }

        public byte[] LeftRotation()
        {
            _myimage = processor.LeftRotation(_myimage);
            return ImageToByteArray();
        }

        public byte[] RightRotation()
        {
            _myimage = processor.RightRotation(_myimage);
            return ImageToByteArray();
        }

        public string GetImageFormat()
        {
            string format = "image/" + _format.ToString();
            return format;
        }

        public bool EmptyImage()
        {
            return string.IsNullOrEmpty(_myImageUrl);
        }

        private byte[] ImageToByteArray()
        {
            MemoryStream ms2 = new MemoryStream();
            ImageFormat format= processor.GetImageFormat(_myimage);
            _myimage.Save(ms2, _format);
            return ms2.ToArray();
        }
    }
}
