using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.Geocoding.Interface {
  public interface IGeocodingProvider {
    IGeometry Geocode(string full_address);
    IGeometry Geocode(string city, string state);
    IGeometry Geocode(string street, string city, string state);
  }
}
