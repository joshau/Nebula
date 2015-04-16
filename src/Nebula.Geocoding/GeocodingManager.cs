using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Geocoding {
  public class GeocodingManager {

    IList<Interface.IGeocodingProvider> providers;

    public GeocodingManager(IList<Interface.IGeocodingProvider> geocoding_providers) {
      this.providers = geocoding_providers;
    }

    public Interface.IGeometry FindGeometry(string address) {
      
      Interface.IGeometry geometry = null;
      int i = 0;

      while ((geometry == null || !geometry.has_results) && i < this.providers.Count) {
        geometry = this.providers[i].Geocode(address);
        i++;
      }

      return geometry;
    }

    public Interface.IGeometry FindGeometry(string city, string state) {

      Interface.IGeometry geometry = null;
      int i = 0;

      while ((geometry == null || !geometry.has_results) && i < this.providers.Count) {
        geometry = this.providers[i].Geocode(city, state);
        i++;
      }

      return geometry;
    }

    public Interface.IGeometry FindGeometry(string street, string city, string state) {

      Interface.IGeometry geometry = null;
      int i = 0;

      while ((geometry == null || !geometry.has_results) && i < this.providers.Count) {
        geometry = this.providers[i].Geocode(street, city, state);
        i++;
      }

      return geometry;
    }
  }
}
