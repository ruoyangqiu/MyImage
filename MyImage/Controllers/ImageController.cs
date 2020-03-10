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
using static MyImage.Data.ErrorMessages;

namespace MyImage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        
        private MyImageServer _imageserver = MyImageServer.Instance;

        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(InvalidUriError), 400)]
        public ActionResult Create(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                return StatusCode(400, new InvalidUriError());
            }
            bool validurl = _imageserver.InitializeImage(url);
            if(!validurl)
            {
                return StatusCode(400, new InvalidUriError());
            }
            return File(_imageserver.GetDisplay(), _imageserver.GetImageFormat());
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult Get()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.GetDisplay(), _imageserver.GetImageFormat());
        }

        [HttpGet("anglerotation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult RotateByAngle(int angle)
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.GetRotationByAngle(angle), _imageserver.GetImageFormat());
        }

        [HttpGet("flipping")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        [ProducesResponseType(typeof(InvalidOrientationError), 400)]
        public ActionResult Filp(string orientation)
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            if (!orientation.Equals("horizontal", StringComparison.InvariantCultureIgnoreCase) && !orientation.Equals("vertical", StringComparison.InvariantCultureIgnoreCase))
            {
                return StatusCode(400, new InvalidOrientationError());
            }

            return File(_imageserver.GetFlipping(orientation), _imageserver.GetImageFormat());
        }

        [HttpGet("grayscale")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult Greyscale()
        {
            if(_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.GetGrayScale(), _imageserver.GetImageFormat());
        }

        [HttpGet("resizing")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        [ProducesResponseType(typeof(InvalidWidthError), 400)]
        [ProducesResponseType(typeof(InvalidHeightError), 400)]
        public ActionResult Resize(int width, int height)
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }

            if(width <= 0)
            {
                return StatusCode(400, new InvalidWidthError());
            }

            if (height <= 0)
            {
                return StatusCode(400, new InvalidHeightError());
            }

            return File(_imageserver.GetResize(width, height), _imageserver.GetImageFormat());
        }

        [HttpGet("thumbnail")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult Thumbnail()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.GetTumbnail(), _imageserver.GetImageFormat());
        }

        [HttpGet("leftrotation")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult LeftRotation()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.LeftRotation(), _imageserver.GetImageFormat());
        }

        [HttpGet("rightrotation")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult RightRotation()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.RightRotation(), _imageserver.GetImageFormat());
        }

    }
}
