using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.ImageProcessor
{
    interface IMyImage
    {
        Image display(string url);

        Image AngleRotation(Image img, int angle);

        Image Rotation(Image img);

        Image Fliping(Image img, string orientation);

        Image GrayScale(Image img);

        Image Resize(Image img, int newwidth, int newheight);
    }
}
