using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.ImageProcessor
{
    interface IMyImage
    {
        Image Initialize(Image img);

        Image AngleRotation(Image img, int angle);

        Image Rotation(Image img);

        Image Fliping(Image img, string orientation);

        Image GrayScale(Image img);

        Image Resize(Image img, int newwidth, int newheight);

        Image Thumbnail(Image img);

        ImageFormat GetImageFormat(Image img);

    }
}
