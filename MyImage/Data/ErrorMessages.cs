using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.Data
{
    public class ErrorMessages
    {
        public class InvalidUriError
        {
            public int ErrorCode { get => 101; }

            public string Message { get => "The URI is Invalid!"; }
        }

        public class NoImageError
        {
            public int ErrorCode { get => 102; }

            public string Message { get => "You should upload an image first!"; }
        }

        public class InvalidOrientationError
        {
            public int ErrorCode { get => 103; }

            public string Message { get => "Please Use 'Vertical' or 'Horizontal' as Parameter!"; } 
        }

        public class InvalidWidthError
        {
            public int ErrorCode { get => 104; }

            public string Message { get => "Width must be positive!"; }
        }

        public class InvalidHeightError
        {
            public int ErrorCode { get => 105; }

            public string Message { get => "Height must be positive!"; }
        }

        public class EmptyParameterError
        {
            public int ErrorCode { get => 106; }

            public string Message { get => "The Parameter is empty!"; }
        }
    }
}
