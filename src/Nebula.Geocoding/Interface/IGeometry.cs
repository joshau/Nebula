using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Geocoding.Interface {
  public interface IGeometry {
    bool has_results { get; }
    bool is_valid { get; }
    string error_message { get; }
    double latitude { get; }
    double longitude { get; }
  }
}
