using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return StatusCode(200);
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

        [HttpPut("anglerotation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult RotateByAngle(int angle)
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            if (angle != 0)
            {
                _imageserver.GetRotationByAngle(angle);
            }
            
            return StatusCode(200);
        }

        [HttpPut("flipping")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        [ProducesResponseType(typeof(InvalidOrientationError), 400)]
        [ProducesResponseType(typeof(EmptyParameterError), 400)]
        public ActionResult Filp(string orientation)
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            if(string.IsNullOrEmpty(orientation))
            {
                return StatusCode(400, new EmptyParameterError());
            }
            if (!orientation.Equals("horizontal", StringComparison.InvariantCultureIgnoreCase) && !orientation.Equals("vertical", StringComparison.InvariantCultureIgnoreCase))
            {
                return StatusCode(400, new InvalidOrientationError());
            }
            _imageserver.GetFlipping(orientation);
            return StatusCode(200);
        }

        [HttpPut("grayscale")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult Greyscale()
        {
            if(_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            _imageserver.GetGrayScale();
            return StatusCode(200);
        }

        [HttpPut("resizing")]
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
            _imageserver.GetResize(width, height);
            return StatusCode(200);
        }

        [HttpPut("thumbnail")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult Thumbnail()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            _imageserver.GetTumbnail();
            return StatusCode(200);
        }

        [HttpGet("thumbnail")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult GetThumbnail()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            return File(_imageserver.GetTumbnail(), _imageserver.GetImageFormat());
        }

        [HttpPut("leftrotation")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult LeftRotation()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            _imageserver.LeftRotation();
            return StatusCode(200);
        }

        [HttpPut("rightrotation")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult RightRotation()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            _imageserver.RightRotation();
            return StatusCode(200);
        }

    }
}
