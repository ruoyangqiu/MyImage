using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

namespace MyImage.ImageProcessor
{
    public class MyImageType : IMyImage
    {
        [JsonIgnore]
        private static Image OriginalImage;
        private static int OriginalWidth;
        private static int OriginalHeight;
        //private static Bitmap OriginalBitmap;

        //public MyImageType(string imageuri)
        //{
        //    this.ImageURI = imageuri;
        //    OriginalImage = Image.FromFile(this.ImageURI);
        //}
        public Image Initialize(Image img)
        {
            OriginalImage = img;
            OriginalWidth = img.Width;
            OriginalHeight = img.Height;
            return img;
        }

        public Image AngleRotation(Image img, int angle)
        {
            Bitmap bmp = new Bitmap(img);
            img = (Image)RotateImage(bmp, (float)angle);
            return img;
        }

        public Image Rotation(Image img)
        {
            return AngleRotation(img, 90);
        }

        public Image Rotation(string url)
        {
            throw new NotImplementedException();
        }

        public Image Fliping(Image img, string orientation)
        {
            //Image flip = convertImageFromWebUri(url);
            if(orientation.Equals("vertical", StringComparison.InvariantCultureIgnoreCase))
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            if(orientation.Equals("horizontal", StringComparison.InvariantCultureIgnoreCase))
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            return img;
        }

        public Image GrayScale(Image img)
        {
            return MakeGrayScale(img);
        }

        public Image Resize(Image img, int newwidth, int newheight)
        {
            return MakeResize(img, newwidth, newheight);
        }

        public ImageFormat GetImageFormat(Image img)
        {
            return img.RawFormat;
        }


        private static Bitmap RotateImage(Bitmap bmp, float angle)
        {
            float alpha = 45;

            //edit: negative angle +360
            while (alpha < 0) alpha += 360;

            float gamma = 90;
            float beta = 180 - 45 - gamma;

            float c1 = OriginalHeight;
            //float a1 = (float)(c1 * Math.Sin(alpha * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            //float b1 = (float)(c1 * Math.Sin(beta * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));

            //float c2 = OriginalWidth;
            //float a2 = (float)(c2 * Math.Sin(alpha * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            //float b2 = (float)(c2 * Math.Sin(beta * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));

            float c3 = (float)Math.Sqrt(Math.Pow(OriginalHeight, 2) + Math.Pow(OriginalWidth, 2));

            int width = Convert.ToInt32(c3);
            int height = Convert.ToInt32(c3);

            Bitmap rotatedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(rotatedImage.Width / 2, rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-rotatedImage.Width / 2, -rotatedImage.Height / 2); //restore rotation point into the matrix
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Point((width - bmp.Width) / 2, (height - bmp.Height) / 2)); //draw the image on the new bitmap
            }
            return rotatedImage;
        }

        private Image MakeGrayScale(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            for(int i = 0; i < img.Width; i ++)
            {
                for(int j = 0; j < img.Height; j++)
                {
                    Color OriginalColor = bmp.GetPixel(i, j);
                    int grayscale = (int)((OriginalColor.R * 0.3) + (OriginalColor.G *
                    0.59) + (OriginalColor.B * 0.11));
                    Color NewColor = Color.FromArgb(grayscale, grayscale, grayscale);
                    bmp.SetPixel(i, j, NewColor);
                }
            }
            return (Image)bmp;
        }

        private Image MakeResize(Image img, int newwidth, int newheight)
        {
            var box = new Rectangle(0, 0, newwidth, newheight);
            Bitmap bmp = new Bitmap(newwidth, newheight);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using(Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using(var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(img, box, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return (Image)bmp;
        }
    }
}
