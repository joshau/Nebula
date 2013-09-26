using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nebula.Facebook {
    public class Facebook {

        private const string FACEBOOK_GRAPH_ACCESS_TOKEN_URL = "https://graph.facebook.com/oauth/access_token";

        private Regex regex_access_token;
        private Regex regex_access_token_expires;

        public long id { get; set; }
        public string secret { get; set; }

        public Facebook(long id, string secret) {

            this.id = id;
            this.secret = secret;

            this.regex_access_token = new Regex("access_token=(.+)");
            this.regex_access_token_expires = new Regex("access_token=(.+)&expires=(.+)");
        }

        public string GetAccessToken() {

            string url = String.Format("{1}?type=client_cred&client_id={1}+&client_secret={2}", FACEBOOK_GRAPH_ACCESS_TOKEN_URL, this.id.ToString(), this.secret);
            string access_token = this.regex_access_token.Match(Utility.GetWebResponse(url)).Groups[1].Value;
            
            return access_token;
        }

        public string ExchangeCodeForAccessToken(string code, string redirect_url) {

            string url = string.Format("{0}?client_id={1}&redirect_uri={2}&client_secret={3}&code={4}", 
                FACEBOOK_GRAPH_ACCESS_TOKEN_URL, this.id.ToString(), redirect_url, this.secret, code);

            Match regex_match = this.regex_access_token_expires.Match(Utility.GetWebResponse(url));

            string access_token = (regex_match.Success && regex_match.Groups.Count > 0) ? regex_match.Groups[1].Value : null;
            long expires = (regex_match.Success && regex_match.Groups.Count > 1) ? Convert.ToInt64(regex_match.Groups[2].Value) : 0;

            return access_token;
        }
    }
}
