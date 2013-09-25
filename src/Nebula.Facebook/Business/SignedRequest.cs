using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Business {
    public class SignedRequest {

        //{
        //   "algorithm": "HMAC-SHA256",
        //   "expires": 1380128400,
        //   "issued_at": 1380123830,
        //   "oauth_token": "CAAGTY6ddm3ABAKL8N7m2ZAdMhIpv2twUmLCgALNMzKFZCy4f2lPXAZAAhkZBbMtWFJqIq3S6hYmo0Cwmwv7GlsQotx32pe2CBpyxIBIJQeCObZC2SRFpQG4uqoVBvwFBFzDZA847OY0OpujnWubV0rz1M0PTamoqO2XSkEOTpCwd0M52qBAhM59iIOYjUQUB2TUMioVbypiAZDZD",
        //   "page": {
        //      "id": "111014669977",
        //      "liked": false,
        //      "admin": true
        //   },
        //   "user": {
        //      "country": "us",
        //      "locale": "en_US",
        //      "age": {
        //         "min": 21
        //      }
        //   },
        //   "user_id": "100001486925330"
        //}

        public SignedRequestUser user { get; set; }
        public string algorithm { get; set; }
        public double issued_at { get; set; }
        public long user_id { get; set; }
        public string oauth_token { get; set; }
        public double expires { get; set; }
        public string app_data { get; set; }
        public SignedRequestPage page { get; set; }        

        public static SignedRequest Create(string signedRequest, string facebookAppSecretKey) {

            if (signedRequest == null) return null;

            string[] signedRequestSplit = signedRequest.Split('.');
            string encodedSignature = signedRequestSplit[0];
            string payload = signedRequestSplit[1];
            string signature = Utility.Base64UrlDecode(encodedSignature);
            string data = Utility.Base64UrlDecode(payload);
            SignedRequest signReq = Utility.DeserializeSignedRequest(data);

            if(signReq.algorithm.ToUpper() != "HMAC-SHA256") {
                throw new Exceptions.InvalidAlgorithmException(String.Format("Unknown algorithm ({0}). Expected {1}.", signReq.algorithm.ToUpper(), "HMAC-SHA256"));
            }
            if(signature != Utility.Base64UrlDecode(Utility.GeneratorSignatureHash(payload, facebookAppSecretKey))) {
                throw new Exceptions.InvalidSignedRequestException("Bad JSON signature!");
            }

            return signReq;
        }
    }
}
