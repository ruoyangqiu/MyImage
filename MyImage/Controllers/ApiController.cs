using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyImage.Data;
using MyImage.ImageProcessor;
using MyImage.Service;

namespace MyImage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        

        private IMyImage _myimage = new MyImageType();
        //private List<string> _imagelist = new List<string>();
        //private string _imageurl = "";
        //private ImageMock im = ImageMock.Instance;
        private MyImageServer _imageserver = new MyImageServer();
        //private MemoryStream _imagestream;
        private static Image _image;
        //private Image _image;

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        

        [HttpPost]
        public ActionResult Create(string url)
        {

            _imageserver.datastore.add(url);
            _image = _myimage.display(_imageserver.datastore.url);
            //im.add(_imageurl);
            Console.WriteLine(_imageserver.datastore.url);
            return File(ImageToByteArray(), "image/jpg");
        }

        [HttpGet("anglerotation")]
        public ActionResult RotateByAngle(int angle)
        {
            //PictureBox pictureBox1 = new PictureBox();
            
            //string url = img.RotationByAngle(70);
            //var image = System.IO.File.OpenRead(url);
            _image = _myimage.AngleRotation(_image, angle);
            Byte[] b;
            b = ImageToByteArray();
            return File(b, "image/png");
        }
        [HttpGet("flipping")]
        public ActionResult Filp(string orientation)
        {
            if(!orientation.Equals("horizontal", StringComparison.InvariantCultureIgnoreCase) && !orientation.Equals("vertical", StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }
            _image = _myimage.Fliping(_image, orientation);
            return File(ImageToByteArray(), "image/png");
        }

        [HttpGet("grayscale")]
        public ActionResult Greyscale()
        {
            _image = _myimage.GrayScale(_image);
            return File(ImageToByteArray(), "image/jpeg");
        }

        private byte[] ImageToByteArray()
        {
            MemoryStream ms2 = new MemoryStream();
            _image.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            return ms2.ToArray();
        }
    }
}
