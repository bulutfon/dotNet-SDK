namespace Bulutfon.Api.Models {

    /// <summary>
    /// Mesajın alıcısı
    /// </summary>
    public class MessageRecipient
    {
        /// <summary>
        /// Alıcı numara
        /// </summary>
        public long number { get; set; }

        /// <summary>
        /// İletilme durumu
        /// WAITING: İletim raporu bekleniyor
        /// FAILED: İletilemedi
        /// CONFIRMED: İletildi
        /// </summary>
        public string state { get; set; }
    }
}