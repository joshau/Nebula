using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Interface {
    public interface IFacebook {

        string GetApplicationAccessToken();
        string ExchangeCodeForAccessToken(string code, string redirect_url);
        bool VerifyUserAccessToken(long user_id, string user_access_token, string app_access_token);
    }
}
