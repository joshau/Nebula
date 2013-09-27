using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Interface {
    public interface IFacebook {

        public string GetApplicationAccessToken();
        public string ExchangeCodeForAccessToken(string code, string redirect_url);
        public bool VerifyUserAccessToken(long user_id, string user_access_token, string app_access_token);
    }
}
