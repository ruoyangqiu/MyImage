using MyImage.Service;
using System;


namespace MyImage.Data
{
    public class ImageMock
    {
        public string guid = new Guid().ToString();

        public MyImageServer _myimage = MyImageServer.Instance;
    }
}
