using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace Nebula.Facebook {
    internal class Utility : Nebula.Utility {

        public static string Base64UrlDecode(string s) {

            byte[] bytes;
            s = s.Replace("=", String.Empty).Replace("-", "+").Replace("_", "/");
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            bytes = Convert.FromBase64String(s);

            return Encoding.UTF8.GetString(bytes);
        }

        public static Business.SignedRequest DeserializeSignedRequest(string json) {
            return (new JavaScriptSerializer()).Deserialize<Business.SignedRequest>(json);
        }

        public static string GeneratorSignatureHash(string payload, string secretKey) {

            byte[] bytesKey = Encoding.UTF8.GetBytes(secretKey);
            byte[] bytesPayload = Encoding.UTF8.GetBytes(payload);
            HMACSHA256 hmacSha256 = new HMACSHA256(bytesKey);

            return Convert.ToBase64String(hmacSha256.ComputeHash(bytesPayload));
        }
    }
}
