
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
        /// <summary>
        /// Singleton
        /// </summary>
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

        #region OriginalImageProperties
        public static string _myImageUrl;
        private static Image _myimage;
        private static ImageFormat _format;
        private static int OriginalWidth;
        private static int OriginalHeight;
        private static bool IsThumbnail;
        private static Image thumb;
        private IMyImage processor = new MyImageType();
        #endregion OriginalImageProperties

        /// <summary>
        /// Initialize ImageServer
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool InitializeImage(string url)
        {
            WebClient wc = new WebClient();
            byte[] bytes;
            try
            {
                 bytes= wc.DownloadData(url);
            } catch(Exception)
            {
                return false;
            }
            MemoryStream ms = new MemoryStream(bytes);
            _myimage = Image.FromStream(ms);
            _myImageUrl = url;
            _format = _myimage.RawFormat;
            OriginalHeight = _myimage.Height;
            OriginalWidth = _myimage.Width;
            IsThumbnail = false;
            //processor.Initialize(_myimage);
            return true;
        }

        /// <summary>
        /// Return actural Image
        /// </summary>
        /// <returns></returns>
        public byte[] GetDisplay()
        {
            return ImageToByteArray();
        }

        public byte[] GetRotationByAngle(float angle)
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

        public bool generateThumb()
        {
            thumb = processor.Thumbnail(_myimage);
            IsThumbnail = true;
            return true;
        }


        public byte[] GetTumbnail()
        {
            //thumb = processor.Thumbnail(_myimage);
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

        public bool HasThumbNail()
        {
            return IsThumbnail;
        }

        public bool EmptyImage()
        {
            return _myimage == null;
        }

        // Convert Image to a byte array for display
        private byte[] ImageToByteArray()
        {
            MemoryStream ms2 = new MemoryStream();
            ImageFormat format= processor.GetImageFormat(_myimage);
            _myimage.Save(ms2, _format);
            return ms2.ToArray();
        }
    }
}
