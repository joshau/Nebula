using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook {
    public class FacebookTab : Interface.IFacebookTab {

        private Business.SignedRequest signed_request { get; set; }

        public FacebookTab(Business.SignedRequest signed_request) {
            this.signed_request = signed_request;
        }

        public bool IsAccessible() {
            return !(this.signed_request == null || this.signed_request.page == null);
        }

        public bool HasUserLikedPage() {
            return this.IsAccessible() ? this.signed_request.page.liked : false;
        }

        public long GetPageId() {
            return this.IsAccessible() ? this.signed_request.page.id : 0;
        }

        public long GetUserId() {
            return this.signed_request.user_id;
        }

        public string GetUserCountry() {
            return this.signed_request.user.country;
        }

        public string GetUserAppData() {
            return this.signed_request.app_data;
        }

        public DateTime? GetIssuedDate() {
            return this.signed_request == null ? (DateTime?)null : GetDateTimeFromOrigin(this.signed_request.issued_at);
        }

        public string GetOAuthToken() {
            return this.signed_request == null ? null : this.signed_request.oauth_token;
        }

        public DateTime? GetOAuthEpiresDate() {
            return this.signed_request == null ? (DateTime?)null : GetDateTimeFromOrigin(this.signed_request.expires);
        }

        private DateTime? GetDateTimeFromOrigin(double seconds) {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(seconds);
        }
    }
}
