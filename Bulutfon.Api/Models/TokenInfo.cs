namespace Bulutfon.Sdk.Models
{
    public class TokenInfo
    {
        /// <summary>
        /// Kullanılan Token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Token Expire oldu mu 
        /// </summary>
        public bool expired { get; set; }

        /// <summary>
        /// Saniye cinsinden tokenın expire süresi
        /// </summary>
        public int expires_in { get; set; }
    }
}
