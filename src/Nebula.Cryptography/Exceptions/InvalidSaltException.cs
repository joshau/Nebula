using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.Cryptography.Exceptions {
  public class InvalidSaltException : Exception {
    public InvalidSaltException() : base() { }
    public InvalidSaltException(string message) : base(message) { }
    public InvalidSaltException(string message, Exception innerException) : base(message, innerException) { }
  }
}
