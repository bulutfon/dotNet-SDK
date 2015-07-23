using System;

namespace Bulutfon.OAuth {

    public class TokenProvider {

        public const string Key = "token_provider";

        public TokenProvider(string accesToken, string refreshToken) {
            AccessToken = accesToken;
            RefreshToken = refreshToken;
        }

        public event TokenExpiredEvent TokenExpired;

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public void RefreshAccessToken() {
            if (TokenExpired != null) {
                var e = new TokenExpiredEventArgs();
                e.RefreshToken = RefreshToken;
                TokenExpired(this, e);
            }
        }
    }
}