using MyImage.Service;
using System;


namespace MyImage.Data
{
    public class ImageMock
    {
        public string guid = new Guid().ToString();

        public MyImageService _myimage = MyImageService.Instance;
    }
}
