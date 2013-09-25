using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Exceptions {
    public class InvalidSignedRequestException : Exception {
        public InvalidSignedRequestException() : base() { }
        public InvalidSignedRequestException(string message) : base(message) { }
    }
}
