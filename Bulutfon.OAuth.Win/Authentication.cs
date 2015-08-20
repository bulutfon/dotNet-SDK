using System.Collections.Generic;

namespace Bulutfon.OAuth.Win {

    /// <summary>
    /// Token'ların ve kullanıcı bilgilerinin saklanması için 
    /// </summary>
    public static class Authentication {

        public static Token Token { get; set; }

        public static string UserName { get; set; }

        public static IDictionary<string, string> UserInfo { get; set; }
    }
}