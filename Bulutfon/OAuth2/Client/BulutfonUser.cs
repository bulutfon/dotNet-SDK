using OAuth2.Models;

namespace Bulutfon.OAuth2.Client
{
    /// <summary>
    /// Bulutfon kullanıcı bilgileri
    /// </summary>
    public class BulutfonUser : UserInfo
    {
        /// <summary>
        /// Ülkesi
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
    }
}