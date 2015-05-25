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
        /// GSM Numarası
        /// </summary>
        public string Gsm { get; set; }

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