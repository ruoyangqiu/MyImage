using MyImage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.Service
{
    public class MyImageServer
    {
        public ImageMock datastore = ImageMock.Instance;
    }
}
