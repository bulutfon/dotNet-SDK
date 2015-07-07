using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bulutfon.MVC4.OAuth {

    /// <summary>
    /// Bulutfon OAuth Client
    /// </summary>
    public class BulutfonClient : OAuth2Client {

        #region Sabitler ve alanlar

        /// <summary>
        /// Authorization endpoint.
        /// </summary>
        private const string AuthorizationEndpoint = "https://www.bulutfon.com/oauth/authorize";

        /// <summary>
        /// Token endpoint.
        /// </summary>
        private const string TokenEndpoint = "https://www.bulutfon.com/oauth/token";

        /// <summary>
        /// User info endpoint.
        /// </summary>
        private const string UserDetailsEndPoint = "https://api.bulutfon.com/me";

        #endregion

        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string[] requestedScopes;

        public BulutfonClient(string clientId, string clientSecret) : this("Bulutfon", clientId, clientSecret, new string[] {}) {
        }

        public BulutfonClient(string clientId, string clientSecret, params string[] requestedScopes)
            : this("Bulutfon", clientId, clientSecret, requestedScopes)
        {
        }

        protected BulutfonClient(string providerName, string clientId, string clientSecret, string[] requestedScopes)
            : base(providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName)){
                throw new ArgumentNullException("providerName");
            }
            if (string.IsNullOrWhiteSpace(clientId)){
                throw new ArgumentNullException("clientId");
            }
            if (string.IsNullOrWhiteSpace(clientSecret)){
                throw new ArgumentNullException("clientSecret");
            }
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.requestedScopes = requestedScopes;
        }
        
        protected override Uri GetServiceLoginUrl(Uri returnUrl) {
            var builder = new UriBuilder(AuthorizationEndpoint);
            builder.AppendQueryArgument("response_type", "code");
            builder.AppendQueryArgument("client_id", this.clientId);
            builder.AppendQueryArgument("redirect_uri", returnUrl.AbsoluteUri);
            return builder.Uri;
        }
        
        protected override IDictionary<string, string> GetUserData(string accessToken) {
            const string args = "?access_token=";
            using (WebClient client = new WebClient()) {
                string str = client.DownloadString(UserDetailsEndPoint + args + accessToken);
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

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode) {
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", this.clientId);
            dic.Add("redirect_uri", returnUrl.AbsoluteUri);
            dic.Add("client_secret", this.clientSecret);
            dic.Add("code", authorizationCode);
            dic.Add("scope", "cdr");
            dic.Add("grant_type", "authorization_code");

            string postData = "";
            string postDataSperator = "";
            foreach(var i in dic) {
                postData += string.Format("{0}{1}={2}", postDataSperator, HttpUtility.UrlEncode(i.Key), HttpUtility.UrlEncode(i.Value));
                postDataSperator = "&";
            }
            using (WebClient client = new WebClient()) {
                //client.Headers["ContentType"] = "application/x-www-form-urlencoded";
                string str = client.UploadString(TokenEndpoint, postData);
                if (string.IsNullOrEmpty(str)) {
                    return null;
                }
                JObject JSonResult = JObject.Parse(str);
                return JSonResult.GetValue("access_token").ToString();
            }
        }

        //TODO bu metod override yapılmadan çağırılabilir mi?
        public override AuthenticationResult VerifyAuthentication(HttpContextBase context, Uri returnPageUrl) {
            string code = context.Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
                return AuthenticationResult.Failed;

            string accessToken = this.QueryAccessToken(returnPageUrl, code);

            //TODO session nesnesinin içinden bir alanın kullanılması bir bileşen için iyi bir yöntem değil. 
            //Bu nedenle "token" bilgisi user yada benzeri bir nesne üzerinden alınması ya da session üzerindeki key'in "Bulutfon_User_token" gibi bir key ile tutulması daha uygun olacaktır.
            context.Session["token"] = accessToken;

            if (accessToken == null)
                return AuthenticationResult.Failed;

            IDictionary<string, string> userData = this.GetUserData(accessToken);
            if (userData == null)
                return AuthenticationResult.Failed;

            string id = userData["user_id"];
            string name = userData["user_name"];

            return new AuthenticationResult(
                isSuccessful: true, provider: "Bulutfon", providerUserId: id, userName: name, extraData: userData);
        }
    }
}