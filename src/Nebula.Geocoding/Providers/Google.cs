using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Json;

namespace Nebula.Geocoding.Providers
{
    public class Google : Abstract.Geocoding, Interface.IGeocodingProvider
    {
      public enum Format {
        JSON,
        XML
      }

      private const string ENDPOINT = "https://maps.googleapis.com/maps/api/geocode/{0}?address={1}&key={2}";      

      private string api_key;
      private Format response_format;
      
      public Google(string api_key) {
        this.api_key = api_key;
        this.response_format = Format.JSON;
      }

      public Interface.IGeometry Geocode(string full_address) {
        string url = this.CreateUrl(full_address);
        string response = Nebula.Utility.GetWebResponse(url);
        return this.Deserialize(response);
      }

      public Interface.IGeometry Geocode(string city, string state) {
        string url = this.CreateUrl(string.Format("{0}, {1}", city.Trim(), state.Trim()));
        string response = Nebula.Utility.GetWebResponse(url);
        return this.Deserialize(response);
      }

      public Interface.IGeometry Geocode(string street, string city, string state) {
        string url = this.CreateUrl(string.Format("{0} {1}, {2}", street.Trim(), city.Trim(), state.Trim()));
        string response = Nebula.Utility.GetWebResponse(url);
        return this.Deserialize(response);
      }

      private string CreateUrl(string parameters) {
        return string.Format(ENDPOINT, this.response_format == Format.JSON ? "json" : "xml", parameters.Replace(' ', '+'), this.api_key);
      }

      private Business.GeometryGoogle Deserialize(string json) {
        Business.GeometryGoogle geometry = this.Deserialize<Business.GeometryGoogle>(json);
        return geometry;
      }
    }
}
