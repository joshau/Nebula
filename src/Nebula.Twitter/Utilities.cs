using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Nebula.Twitter {
    public class Utilities {

        /// <summary>
        /// Fetchs a Twitter Application Bearer Token
        /// Implemented according to https://dev.twitter.com/docs/auth/application-only-auth
        /// </summary>
        /// <param name="consumerKey">Twitter Application Consumer Key</param>
        /// <param name="consumerSecret">Twitter Application Consumer Secret</param>
        /// <returns>Application Bearer Access Token</returns>
        public static string GetApplicationBearerToken(string consumerKey, string consumerSecret) {

            string token;

            string bearerTokenCreds = string.Format("{0}:{1}", consumerKey, consumerSecret);
            byte[] bearerTokenCredsBytes = Encoding.UTF8.GetBytes(bearerTokenCreds.ToCharArray());
            string bearerTokenCredsBase64 = Convert.ToBase64String(bearerTokenCredsBytes);

            WebRequest request = WebRequest.Create("https://api.twitter.com//oauth2/token");
            byte[] body = Encoding.UTF8.GetBytes("grant_type=client_credentials");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

            request.Headers.Add("Authorization", String.Format("Basic {0}", bearerTokenCredsBase64));
            request.GetRequestStream().Write(body, 0, body.Length);

            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream())) {
                token = reader.ReadToEnd();
            }

            return Regex.Match(token, "\"access_token\":\"(.+)\"").Groups[1].Value;
        }
    }
}
