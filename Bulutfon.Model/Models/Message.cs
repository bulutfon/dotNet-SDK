using System.Collections.Generic;

namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Mesaj bilgileri
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Mesajın id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Mesaj başlığı
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Mesaj metni
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Oluşturma tarihi
        /// </summary>
        public string created_at { get; set; }
        
        /// <summary>
        /// Uzun mesajlar tek parça halinde mi iletilecek
        /// </summary>
        public bool sent_as_single_sms { get; set; }

        /// <summary>
        /// İleri tarihli gönderilecek mesaj mı?
        /// </summary>
        public bool is_planned_sms { get; set; }
        
        /// <summary>
        /// İleri tarihli mesajların gönderileceği zaman
        /// </summary>
        public object send_date { get; set; }
        
        /// <summary>
        /// Alıcılar
        /// </summary>
        public List<MessageRecipient> recipients { get; set; }
    }
}