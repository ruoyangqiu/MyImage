using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.Net;
using System.IO;

namespace MyImage.ImageProcessor
{
    public class MyImageType : IMyImage
    {
        [JsonIgnore]
        private Image OriginalImage { get; set; }

        //public MyImageType(string imageuri)
        //{
        //    this.ImageURI = imageuri;
        //    OriginalImage = Image.FromFile(this.ImageURI);
        //}
        public Image display(string url)
        {
            return convertImageFromWebUri(url);
        }

        

        public Image AngleRotation(string url, int angle)
        {
            Bitmap bmp = new Bitmap(convertImageFromWebUri(url));
            Image rotate = (Image)rotateImage(bmp, (float)angle);
            return rotate;
        }

        public string Rotation()
        {
            Image rotate = OriginalImage;
            rotate.RotateFlip(RotateFlipType.Rotate90FlipNone);
            string url = "Resource/rotation.jpg";
            //Image rotate = (Image)rotateImage(bmp, (float)angle);
            rotate.Save(url);

            return url;
        }

        public Image Rotation(string url)
        {
            throw new NotImplementedException();
        }

        private Image convertImageFromWebUri (string url)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(bytes);
            Image img = Image.FromStream(ms);
            //ms.Dispose();
            return img;
        }

        private Bitmap rotateImage(Bitmap bmp, float angle)
        {
            float height = bmp.Height;
            float width = bmp.Width;
            int hypotenuse = System.Convert.ToInt32(System.Math.Floor(Math.Sqrt(height * height + width * width)));
            Bitmap rotatedImage = new Bitmap(hypotenuse, hypotenuse);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform((float)rotatedImage.Width / 2, (float)rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-(float)rotatedImage.Width / 2, -(float)rotatedImage.Height / 2); //restore rotation point into the matrix
                g.DrawImage(bmp, (hypotenuse - width) / 2, (hypotenuse - height) / 2, width, height);
            }
            return rotatedImage;
        }
    }
}
