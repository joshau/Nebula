using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Nebula;

namespace Nebula.Facebook {
    public class Facebook : Interface.IFacebook {

        private const string FACEBOOK_GRAPH_ACCESS_TOKEN_URL = "https://graph.facebook.com/oauth/access_token";
        private const string FACEBOOK_GRAPH_DEBUG_TOKEN_URL = "https://graph.facebook.com/debug_token";

        private Utility util;

        public long id { get; private set; }
        public string secret { get; private set; }

        public Facebook(long id, string secret) {

            this.util = new Utility();
            this.id = id;
            this.secret = secret;            
        }

        #region Get Application Access Token

        private string GetGetApplicationAccessTokenUrl() {
            return String.Format("{0}?type=client_cred&client_id={1}+&client_secret={2}",
                FACEBOOK_GRAPH_ACCESS_TOKEN_URL, this.id.ToString(), this.secret);
        }

        public string GetApplicationAccessToken() {

            string url = GetGetApplicationAccessTokenUrl();
            string response = Utility.GetWebResponse(url);

            return GetFacebookToken(response);
        }

        /// <summary>
        /// Get an Application Access Token. Result is a string.
        /// </summary>
        /// <param name="callback"></param>
        public void GetApplicationAccessTokenAsync(EventHandler<Async.FacebookRequestAsyncCompletedEventArgs> callback) {

            string result = null;
            string url = GetGetApplicationAccessTokenUrl();
            string response = Utility.GetWebResponse(url);

            Exception ex = null;

            this.util.GetWebResponseAsync(url, (s, e) => {

                try {
                    result = GetFacebookToken(e.Result);
                }
                catch (Exception exc) {
                    ex = exc;
                }
                
                callback(s, new Async.FacebookRequestAsyncCompletedEventArgs(ex, e.Cancelled, e.UserState, result));
            });
        }

        #endregion

        #region Exchange Code For Access Token

        private string GetExchangeCodeForAccessTokenUrl(string redirect_url, string code) {

            return string.Format("{0}?client_id={1}&redirect_uri={2}&client_secret={3}&code={4}",
                FACEBOOK_GRAPH_ACCESS_TOKEN_URL, this.id.ToString(), redirect_url, this.secret, code);
        }

        public string ExchangeCodeForAccessToken(string code, string redirect_url) {

            string url = GetExchangeCodeForAccessTokenUrl(redirect_url, code);
            string response = Utility.GetWebResponse(url);

            Utility.VerifyPayload(response);

            return GetFacebookToken(response);
        }

        /// <summary>
        /// Exchange Facebook Code for a token. Result is a string.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="redirect_url"></param>
        /// <param name="callback"></param>
        public void ExchangeCodeForAccessTokenAsync(string code, string redirect_url, EventHandler<Async.FacebookRequestAsyncCompletedEventArgs> callback) {

            string result = string.Empty;
            string url = GetExchangeCodeForAccessTokenUrl(redirect_url, code);
            Exception ex = null;

            this.util.GetWebResponseAsync(url, (s, e) => {
                
                try {
                    Utility.VerifyPayload(e.Result);
                }
                catch (Exception exc) {
                    ex = exc;
                }

                result = GetFacebookToken(e.Result);

                callback(s, new Async.FacebookRequestAsyncCompletedEventArgs(ex, e.Cancelled, e.UserState, result));
            });
        }

        #endregion        

        #region Verify User Access Token

        private string GetVerifyUserAccessTokenUrl(string user_access_token, string app_access_token) {
            return string.Format("{0}?input_token={1}&access_token={2}", FACEBOOK_GRAPH_DEBUG_TOKEN_URL, user_access_token, app_access_token);
        }

        private bool VerifyUserAccessToken(dynamic payload, long user_id) {
            bool is_valid = false;

            if (payload != null && payload.data != null && payload.data.user_id != null && payload.data.app_id != null)
                is_valid = (user_id == (long)payload.data.user_id && this.id == (long)payload.data.app_id) && (bool)payload.data.is_valid;

            return is_valid;
        }

        public bool VerifyUserAccessToken(long user_id, string user_access_token, string app_access_token) {

            string url = GetVerifyUserAccessTokenUrl(user_access_token, app_access_token);
            dynamic response = SimpleJson.DeserializeObject(Utility.GetWebResponse(url));

            Utility.VerifyPayload(response);

            return VerifyUserAccessToken(response, user_id);
        }

        /// <summary>
        /// Verify User Token. Result is a boolean.
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="user_access_token"></param>
        /// <param name="app_access_token"></param>
        /// <param name="callback"></param>
        public void VerifyUserAccessTokenAsync(long user_id, string user_access_token, string app_access_token, EventHandler<Async.FacebookRequestAsyncCompletedEventArgs> callback) {

            bool result = false;
            string url = GetVerifyUserAccessTokenUrl(user_access_token, app_access_token);
            Exception ex = null;

            this.util.GetWebResponseAsync(url, (s, e) => {

                dynamic response = null;

                try {
                    response = SimpleJson.DeserializeObject(e.Result);
                    Utility.VerifyPayload(response);
                    result = VerifyUserAccessToken(response, user_id);
                }
                catch (Exception exc) {
                    ex = exc;
                }                

                callback(s, new Async.FacebookRequestAsyncCompletedEventArgs(ex, e.Cancelled, e.UserState, result));
            });
        }

        #endregion
        
        private string GetFacebookToken(string payload) {

            bool has_expires = true;
            Regex regex_access_token = new Regex("access_token=(.+)");
            Regex regex_access_token_expires = new Regex("access_token=(.+)&expires=(.+)");

            Match regex_match = regex_access_token_expires.Match(payload);

            if (!regex_match.Success) {
                regex_match = regex_access_token.Match(payload);
                has_expires = false;
            }
                

            string access_token = (regex_match.Success && regex_match.Groups.Count > 0) ? regex_match.Groups[1].Value : null;
            long expires = has_expires ? Convert.ToInt64(regex_match.Groups[2].Value) : 0;
            
            return access_token;
        }        
    }
}
