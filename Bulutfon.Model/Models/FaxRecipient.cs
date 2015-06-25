namespace Bulutfon.Model.Models
{
    public class FaxRecipient
    {
        /// <summary>
        /// Gönderilecek numara
        /// </summary>
        public long number { get; set; }

        /// <summary>
        /// Gönderilme durumu
        /// SENT: Gönderildi
        /// FAILED: Gönderilemedi
        /// WAITING: Gönderme kuyruğunda
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Örn. NORMAL_CLEARING
        /// </summary>
        public string hangup_cause { get; set; }

        /// <summary>
        /// Örn. send_bye
        /// </summary>
        public string hangup_state { get; set; }
    }
}