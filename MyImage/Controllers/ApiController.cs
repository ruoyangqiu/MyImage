using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyImage.ImageProcessor;

namespace MyImage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private IMyImage _myimage = new MyImageType();
        private string _imageurl;

        private MemoryStream _imagestream;

        private Image _image;

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
            _imageurl = "";
            _imagestream = new MemoryStream();
//_image = new Bitmap("");
        }

        

        [HttpPost]
        public ActionResult Create(string url)
        {
            _imageurl = url;

            Image img = _myimage.display(_imageurl);
            return File(ImageToByteArray(img), "image/png");
        }

        [HttpGet("anglerotation")]
        public ActionResult RotateByAngle(string url, int angle)
        {
            //PictureBox pictureBox1 = new PictureBox();
            
            //string url = img.RotationByAngle(70);
            //var image = System.IO.File.OpenRead(url);
            Image img = _myimage.AngleRotation(url, angle);
            Byte[] b;
            b = ImageToByteArray(img);
            return File(b, "image/png");
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms2 = new MemoryStream();
            imageIn.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            return ms2.ToArray();
        }
    }
}
