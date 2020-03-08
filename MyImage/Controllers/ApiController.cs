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
            Image img = _myimage.display(_imageserver.datastore.url);
            //im.add(_imageurl);
            Console.WriteLine(_imageserver.datastore.url);
            return File(ImageToByteArray(img), "image/png");
        }

        [HttpGet("anglerotation")]
        public ActionResult RotateByAngle(int angle)
        {
            //PictureBox pictureBox1 = new PictureBox();
            
            //string url = img.RotationByAngle(70);
            //var image = System.IO.File.OpenRead(url);
            Image img = _myimage.AngleRotation(_imageserver.datastore.url, angle);
            Byte[] b;
            b = ImageToByteArray(img);
            return File(b, "image/png");
        }
        [HttpGet("flipping")]
        public ActionResult Filp(int orientation)
        {
            if(orientation != 0 && orientation != 1)
            {
                return BadRequest();
            }
            Image img = _myimage.Fliping(_imageserver.datastore.url, orientation);
            return File(ImageToByteArray(img), "image/png");
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms2 = new MemoryStream();
            imageIn.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            return ms2.ToArray();
        }
    }
}
