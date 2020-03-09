﻿using System;
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
    public class ApiController : ControllerBase
    {
        
        private MyImageServer _imageserver = MyImageServer.Instance;

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
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

        [HttpGet("anglerotation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(InvalidUriError), 400)]
        public ActionResult RotateByAngle(int angle)
        {
            return File(_imageserver.GetRotationByAngle(angle), _imageserver.GetImageFormat());
        }

        [HttpGet("flipping")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(InvalidUriError), 400)]
        public ActionResult Filp(string orientation)
        {
            if(!orientation.Equals("horizontal", StringComparison.InvariantCultureIgnoreCase) && !orientation.Equals("vertical", StringComparison.InvariantCultureIgnoreCase))
            {
                return StatusCode(400, new InvalidOrientationError());
            }

            return File(_imageserver.GetFlipping(orientation), _imageserver.GetImageFormat());
        }

        [HttpGet("grayscale")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(InvalidUriError), 400)]
        public ActionResult Greyscale()
        {
            return File(_imageserver.GetGrayScale(), _imageserver.GetImageFormat());
        }

        [HttpGet("resizing")]
        public ActionResult Resize(int width, int height)
        {
            return File(_imageserver.GetResize(width, height), _imageserver.GetImageFormat());
        }

        [HttpGet("thumbnail")]
        public ActionResult Thumbnail()
        {
            return File(_imageserver.GetTumbnail(), _imageserver.GetImageFormat());
        }

        [HttpGet("leftrotation")]
        public ActionResult LeftRotation()
        {
            return File(_imageserver.LeftRotation(), _imageserver.GetImageFormat());
        }

        [HttpGet("rightrotation")]
        public ActionResult RightRotation()
        {
            return File(_imageserver.RightRotation(), _imageserver.GetImageFormat());
        }

    }
}
