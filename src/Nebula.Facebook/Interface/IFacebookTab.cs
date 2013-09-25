using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Interface {
    public interface IFacebookTab {

        bool IsAccessible();
        bool HasUserLikedPage();
        long GetPageId();
        long GetUserId();        
        string GetUserCountry();
        string GetUserAppData();
        DateTime? GetIssuedDate();
        string GetOAuthToken();
        DateTime? GetOAuthEpiresDate();
    }
}
