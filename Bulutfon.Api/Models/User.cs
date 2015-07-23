namespace Bulutfon.Sdk.Models
{
    /// <summary>
    /// Genel kullanıcı bilgileri
    /// </summary>
    public class User
    {
        /// <summary>
        /// Kullanıcının mail adresi
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Kullanıcının ad soyadı
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Kullanıcının cep telefonu
        /// </summary>
        public string gsm { get; set; }
    }
}