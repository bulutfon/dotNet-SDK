using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Bulutfon.OAuth.Win {

    /// <summary>
    /// Bulutfon OAuth masaüstü uygulamalar için authentication
    /// </summary>
    public class BulutfonWinClient {

        protected string ClientId { get; private set; }
        protected string ClientSecret { get; private set; }

        public BulutfonWinClient(string clientId, string clientSecret) {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException("client id ya da client secret belirtilmemiş!");
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string GetServiceLoginUrl() {
            const string template = "https://www.bulutfon.com/oauth/authorize?client_id={0}&response_type=code&" + 
                "redirect_uri=urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob&display=popup";
            return string.Format(template, ClientId);
        }

        private Token QueryAccessTokens(string authorizationCode) {
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", ClientId);
            dic.Add("redirect_uri", "urn:ietf:wg:oauth:2.0:oob");
            dic.Add("client_secret", ClientSecret);
            dic.Add("code", authorizationCode);
            dic.Add("scope", "cdr");
            dic.Add("grant_type", "authorization_code");
            
            string postData = "";
            string postDataSperator = "";
            foreach(var i in dic) {
                postData += string.Format("{0}{1}={2}", 
                    postDataSperator, HttpUtility.UrlEncode(i.Key), HttpUtility.UrlEncode(i.Value));
                postDataSperator = "&";
            }
            using (WebClient client = new WebClient()) {
                string str = client.UploadString(Endpoints.Token, postData);
                if (string.IsNullOrEmpty(str)) {
                    return null;
                }
                JObject jsonResult = JObject.Parse(str);

                var token = new Token(
                    jsonResult.GetValue("access_token").ToString(), jsonResult.GetValue("refresh_token").ToString());

                token.TokenExpired += tokenProviderTokenExpired;

                return token;
            }
        }

        void tokenProviderTokenExpired(object s, TokenExpiredEventArgs e) {
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", ClientId);
            dic.Add("client_secret", ClientSecret);
            dic.Add("refresh_token", e.RefreshToken);
            dic.Add("scope", "cdr");
            dic.Add("grant_type", "refresh_token");
            
            string postData = "";
            string postDataSperator = "";
            foreach(var i in dic) {
                postData += string.Format("{0}{1}={2}", 
                    postDataSperator, HttpUtility.UrlEncode(i.Key), HttpUtility.UrlEncode(i.Value));
                postDataSperator = "&";
            }
            using (WebClient client = new WebClient()) {
                string str = client.UploadString(Endpoints.Token, postData);
                if (string.IsNullOrEmpty(str)) {
                    return;
                }
                JObject jsonResult = JObject.Parse(str);
                ((Token)s).SetAccessToken(jsonResult.GetValue("access_token").ToString());
            }
        }

        public bool VerifyAuthentication(string code) {
            if (string.IsNullOrEmpty(code))
                return false;

            var token = QueryAccessTokens(code);

            Authentication.Token = token;

            if (token == null || token.AccessToken == null)
                return false;

            IDictionary<string, string> userData = GetUserData(token.AccessToken);
            if (userData == null)
                return false;

            Authentication.UserName = userData["user_name"];
            Authentication.UserInfo = userData;

            return true;
        }

        protected IDictionary<string, string> GetUserData(string accessToken) {
            const string args = "?access_token=";
            using (WebClient client = new WebClient()) {
                string str = client.DownloadString(Endpoints.UserDetails + args + accessToken);
                if (string.IsNullOrEmpty(str)) {
                    return null;
                }
                JObject result = JObject.Parse(str);
                var ret = new Dictionary<string, string>();
                ret.Add("user_id", result["user"]["email"].ToString());
                ret.Add("user_name", result["user"]["email"].ToString());
                return ret;
            }
        }
    }
}