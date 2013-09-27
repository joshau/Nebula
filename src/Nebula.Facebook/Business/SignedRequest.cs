using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Nebula.Facebook.Business {
    public class SignedRequest {

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

            JavaScriptSerializer js = new JavaScriptSerializer();
            string[] signedRequestSplit = signedRequest.Split('.');
            string encodedSignature = signedRequestSplit[0];
            string payload = signedRequestSplit[1];
            string signature = Utility.Base64UrlDecode(encodedSignature);
            string data = Utility.Base64UrlDecode(payload);
            SignedRequest signReq = js.Deserialize<Business.SignedRequest>(data);

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
