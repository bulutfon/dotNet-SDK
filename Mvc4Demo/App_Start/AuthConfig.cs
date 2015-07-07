using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bulutfon.MVC4.OAuth;
using Microsoft.Web.WebPages.OAuth;
using Mvc4Demo.Models;

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

            OAuthWebSecurity.RegisterClient(new BulutfonClient(
                clientId:"d68a8d69c16b6ac209980dc5ec7b381933d91c71ca37d83e8e5c64b0ae2f3f9e", 
                clientSecret:"6b9f79ac744ce39a61b1ba236782b7de4d54a96f9f6c43077449cd86c9e9f799"), 
                "Bulutfon", null);
        }
    }
}
