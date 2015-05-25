using OAuth2.Models;

namespace Bulutfon.Models
{
    /// <summary>
    /// Bulutfon kullanıcı bilgileri
    /// </summary>
    public class BulutfonUser : UserInfo
    {
        /// <summary>
        /// Kullanıcının tam adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ülkesi
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// PBX (Santral)
        /// </summary>
        public Pbx Pbx { get; set; }

        /// <summary>
        /// Kalan kredi
        /// </summary>
        public double Credit { get; set; }
    }
}