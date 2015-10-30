using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bulutfon.OAuth;
using Bulutfon.OAuth.Mvc;
using Microsoft.Web.WebPages.OAuth;
using Mvc4Demo.Models;
using System.Web;
namespace Mvc4Demo
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();

            TokenRefreshCallback refreshCallback = new TokenRefreshCallback(tokenRefreshed);

            BulutfonWebClient client = new BulutfonWebClient(
                clientId: "CLIENT_ID",
                clientSecret: "CLIENT_SECRET", refreshCallback: refreshCallback);

            OAuthWebSecurity.RegisterClient(client, "Bulutfon", null);

            Token tkn = new Token("a", "b");
            tkn.RefreshCallback += tokenRefreshed;

        }


        public static void tokenRefreshed(object sender, string access_token, string refreh_token) {
            HttpContext.Current.Application["last_access_token"] = access_token;
            HttpContext.Current.Application["last_refresh_token"] = refreh_token;
        }
    }
}
