using MyImage.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.Data
{
    public class ImageMock
    {
        public string guid = new Guid().ToString();

        public MyImageServer _myimage = MyImageServer.Instance;
    }
}
