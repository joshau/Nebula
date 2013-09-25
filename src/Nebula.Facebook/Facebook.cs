using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nebula.Facebook {
    public class Facebook {

        public long id { get; set; }
        public string secret { get; set; }

        public Facebook(long id, string secret) {

            this.id = id;
            this.secret = secret;
        }

        public string GetAccessToken() {

            string url = String.Format("https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={0}+&client_secret={1}", this.id.ToString(), this.secret);
            Regex regex = new Regex("access_token=(.+)");
            string access_token = regex.Match(Utility.GetWebResponse(url)).Groups[1].Value;
            
            return access_token;
        }
    }
}
