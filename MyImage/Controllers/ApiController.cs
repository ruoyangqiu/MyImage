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
        

        //private IMyImage _myimage = new MyImageType();
        //private List<string> _imagelist = new List<string>();
        //private string _imageurl = "";
        //private ImageMock im = ImageMock.Instance;
        private MyImageServer _imageserver = MyImageServer.Instance;
        //private MemoryStream _imagestream;
        //private static Image _image;
        //private Image _image;

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Create(string url)
        {

            bool validurl = _imageserver.InitializeImage(url);
            if(!validurl)
            {
                return BadRequest();
            }
            return File(_imageserver.GetDisplay(), "image/jpeg");
        }

        [HttpGet("anglerotation")]
        public ActionResult RotateByAngle(int angle)
        {
            return File(_imageserver.GetRotationByAngle(angle), "image/jpeg");
        }
        [HttpGet("flipping")]
        public ActionResult Filp(string orientation)
        {
            if(!orientation.Equals("horizontal", StringComparison.InvariantCultureIgnoreCase) && !orientation.Equals("vertical", StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }
            //_image = _myimage.Fliping(_image, orientation);
            return File(_imageserver.GetFlipping(orientation), "image/jpeg");
        }

        [HttpGet("grayscale")]
        public ActionResult Greyscale()
        {
            //_image = _myimage.GrayScale(_image);
            return File(_imageserver.GetGrayScale(), "image/jpeg");
        }

        [HttpGet("resizing")]
        public ActionResult Resize(int width, int height)
        {
            //_image = _myimage.Resize(_image, weight, height);
            return File(_imageserver.GetResize(width, height), "image/jpeg");
        }

    }
}
