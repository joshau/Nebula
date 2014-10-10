using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.AddressValidation.Interface {
  public interface IValidationList {
    Dictionary<string, List<string>> GetValidationList();
  }
}
