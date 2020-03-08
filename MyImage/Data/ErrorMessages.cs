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
            public int ErrorCode { get => 100; }

            public string Message { get => "The URI is Invalid!"; }
        }
    }
}
