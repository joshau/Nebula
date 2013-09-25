using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Exceptions {
    public class InvalidAlgorithmException : Exception {
        public InvalidAlgorithmException() : base() { }
        public InvalidAlgorithmException(string message) : base(message) { }
    }
}
