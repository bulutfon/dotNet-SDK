﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using Newtonsoft.Json.Linq;

namespace Bulutfon.OAuth.Mvc {

    /// <summary>
    /// Bulutfon OAuth Client
    /// </summary>
    public class BulutfonWebClient : OAuth2Client {

        protected string ClientId { get; private set; }
        protected string ClientSecret { get; private set; }
        protected TokenRefreshCallback RefreshCallback;

        public BulutfonWebClient(string clientId, string clientSecret, TokenRefreshCallback refreshCallback)  : this("Bulutfon", clientId, clientSecret, refreshCallback)
        {
        }

        protected BulutfonWebClient(string providerName, string clientId, string clientSecret, TokenRefreshCallback refreshCallback) : base(providerName)
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException("client id ya da client secret belirtilmemiş!");
            ClientId = clientId;
            ClientSecret = clientSecret;
            RefreshCallback = refreshCallback;
        }
        
        protected override Uri GetServiceLoginUrl(Uri returnUrl) {
            var builder = new UriBuilder(Endpoints.Authorization);
            builder.AppendQueryArgument("response_type", "code");
            builder.AppendQueryArgument("client_id", ClientId);
            builder.AppendQueryArgument("redirect_uri", returnUrl.AbsoluteUri);
            return builder.Uri;
        }
        
        protected override IDictionary<string, string> GetUserData(string accessToken) {
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

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode) {
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", ClientId);
            dic.Add("redirect_uri", returnUrl.AbsoluteUri);
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
                return jsonResult.GetValue("access_token").ToString();
            }
        }

        private Token QueryAccessTokens(Uri returnUrl, string authorizationCode) {
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", ClientId);
            dic.Add("redirect_uri", returnUrl.AbsoluteUri);
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

        public void tokenProviderTokenExpired(object s, TokenExpiredEventArgs e) {
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
                ((Token)s).SetRefreshToken(jsonResult.GetValue("refresh_token").ToString());
                ((Token)s).RefreshCallback += RefreshCallback;
            }
        }

        public override AuthenticationResult VerifyAuthentication(HttpContextBase context, Uri returnPageUrl) {
            string code = context.Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
                return AuthenticationResult.Failed;

            var token = QueryAccessTokens(returnPageUrl, code);

            context.Session[Token.Key] = token;

            if (token == null || token.AccessToken == null)
                return AuthenticationResult.Failed;

            IDictionary<string, string> userData = GetUserData(token.AccessToken);
            if (userData == null)
                return AuthenticationResult.Failed;

            string id = userData["user_id"];
            string name = userData["user_name"];

            return new AuthenticationResult(
                isSuccessful: true, provider: "Bulutfon", providerUserId: id, userName: name, extraData: userData);
        }
    }
}