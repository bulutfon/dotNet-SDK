using System;

namespace Bulutfon.OAuth {

    public class TokenExpiredEventArgs : EventArgs {

        public string RefreshToken { get; set; }
    }
}