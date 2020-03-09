using Microsoft.AspNetCore.Mvc;
using MyImage.Data;
using MyImage.ImageProcessor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyImage.Service
{
    public class MyImageServer
    {
        //public ImageMock datastore = ImageMock.Instance;

        #region Singleton
        private static volatile MyImageServer instance;
        private static readonly object syncRoot = new Object();



        public static MyImageServer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MyImageServer();
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
            processor.Initialize(_myimage);
            return true;
        }

        public byte[] GetDisplay()
        {
            return ImageToByteArray();
        }

        public byte[] GetRotationByAngle(int angle)
        {
            _myimage = processor.AngleRotation(_myimage, angle);
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

        private byte[] ImageToByteArray()
        {
            MemoryStream ms2 = new MemoryStream();
            ImageFormat format= processor.GetImageFormat(_myimage);
            _myimage.Save(ms2, _format);
            return ms2.ToArray();
        }
    }
}
