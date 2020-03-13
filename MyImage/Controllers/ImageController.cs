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
        
        private MyImageService _imageserver = MyImageService.Instance;

        private readonly ILogger<ImageController> _logger;


        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Get the Image
        /// </summary>
        /// <returns></returns>
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

        
        /// <summary>
        /// Flip the image either horizontla or vertical based on user's input
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Make the grey scale of the image
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Resize the image depend on the imput of user
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
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

       
        /// <summary>
        /// Rotate Image 90 degree counterclockwise
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Rotate Image 90 degree clockwise
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Rotate image for any angle
        /// if angle is positive, rotate clockwise
        /// if angle is negative, rotate counter-clockwise
        /// if angle is zero, iimage won't change
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Generate a thumbnail for image
        /// </summary>
        /// <returns></returns>
        [HttpPut("thumbnail")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        public ActionResult Thumbnail()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            _imageserver.generateThumb();
            return StatusCode(200);
        }

        /// <summary>
        /// Get the thumbnail
        /// </summary>
        /// <returns></returns>
        [HttpGet("thumbnail")]
        [ProducesResponseType(typeof(NoImageError), 400)]
        [ProducesResponseType(typeof(NoThumbnailError), 400)]
        public ActionResult GetThumbnail()
        {
            if (_imageserver.EmptyImage())
            {
                return StatusCode(400, new NoImageError());
            }
            if(!_imageserver.HasThumbNail())
            {
                return StatusCode(400, new NoThumbnailError());
            }
            return File(_imageserver.GetTumbnail(), _imageserver.GetImageFormat());
        }

    }
}
