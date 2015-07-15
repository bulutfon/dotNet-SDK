using System;

namespace Bulutfon.MVC4.Api {

    public class TokenExpiredEventArgs : EventArgs {
        public string RefreshToken { get; set; }
    }

    public delegate void TokenExpiredEvent(object s, TokenExpiredEventArgs e);

    public class TokenProvider {

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