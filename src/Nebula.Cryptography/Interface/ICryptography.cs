using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.Cryptography.Interface {
  public interface ICryptography {
    string GenerateSalt(int saltSize, int hashIterations = 10000);
    string CreateHash(string text, string salt);
  }
}
