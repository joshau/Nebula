using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.AddressValidation.Interface {
  public interface IAddressValidator {
    string Sanitize(string address_concat);
    List<string> GetMatches(string address_concat);
  }
}
