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

        public class InvalidOrientationError
        {
            public int ErrorCode { get => 102; }

            public string Message { get => "Please Use 'Vertical' or 'Horizontal' as Parameter!"; } 
        }
    }
}
