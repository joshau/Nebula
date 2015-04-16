using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Nebula.Geocoding.Business {
  [DataContract]
  public class GeometryGoogle : Interface.IGeometry {

    //indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
    private const string STATUS_SUCCESS_OK = "OK";
    //indicates that the geocode was successful but returned no results. This may occur if the geocoder was passed a non-existent address.
    private const string STATUS_SUCCESS_NO_RESULTS = "ZERO_RESULTS";
    //indicates that you are over your quota.
    private const string STATUS_ERROR_OVER_QUERY_LIMIT = "OVER_QUERY_LIMIT";
    //indicates that your request was denied.
    private const string STATUS_ERROR_REQUEST_DENIED = "REQUEST_DENIED";
    //generally indicates that the query (address, components or latlng) is missing.
    private const string STATUS_ERROR_INVALID_REQUEST = "INVALID_REQUEST";
    private const string STATUS_ERROR_UNKNOWN_ERROR = "UNKNOWN_ERROR";

    [DataContract]
    public class AddressComponent {
      [DataMember]
      public string long_name { get; set; }
      [DataMember]
      public string short_name { get; set; }
      [DataMember]
      public string[] types { get; set; }
    }

    [DataContract]
    public class Geocode {
      [DataMember]
      public double lat { get; set; }
      [DataMember]
      public double lng { get; set; }
    }

    [DataContract]
    public class Viewport {
      [DataMember]
      public Geocode northeast { get; set; }
      [DataMember]
      public Geocode southwest { get; set; }
    }

    [DataContract]
    public class Geometry {
      [DataMember]
      public Geocode location { get; set; }
      [DataMember]
      public string location_type { get; set; }
      [DataMember]
      public Viewport viewport { get; set; }
    }    

    [DataContract]
    public class Result {
      [DataMember]
      public AddressComponent[] address_components { get; set; }
      [DataMember]
      public string formatted_address { get; set; }
      [DataMember]
      public Geometry geometry { get; set; }
      [DataMember]
      public string[] types { get; set; }
    }

    [DataMember]
    public Result[] results { get; set; }
    [DataMember]
    public string status { get; set; }
    [DataMember]
    public string error_message { get; set; }

    public double latitude { get { return this.has_results ? this.results.First().geometry.location.lat : 0.0d; } }
    public double longitude { get { return this.has_results ? this.results.First().geometry.location.lng : 0.0d; } }

    public bool has_results { get { return this.status == STATUS_SUCCESS_OK; } }
    public bool is_valid { get { return this.IsValid() && this.results != null; } }

    private bool IsValid() {
      switch (this.status) {
        case STATUS_SUCCESS_OK:
        case STATUS_SUCCESS_NO_RESULTS:
          return true;
        case STATUS_ERROR_OVER_QUERY_LIMIT:
        case STATUS_ERROR_REQUEST_DENIED:
        case STATUS_ERROR_INVALID_REQUEST:
        case STATUS_ERROR_UNKNOWN_ERROR:
          return false;
        default:
          return false;
      }
    }
  }
}
