using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.ImageProcessor
{
    interface IMyImage
    {
        void display();

        Image RotationByAngle(int angle);

        string Rotation();
    }
}
