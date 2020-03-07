﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.ImageProcessor
{
    interface IMyImage
    {
        Image display(string url);

        Image AngleRotation(string url, int angle);

        Image Rotation(string url);

    }
}
