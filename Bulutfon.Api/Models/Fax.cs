using System.Collections.Generic;

namespace Bulutfon.Sdk.Models
{
    public class Fax
    {
        /// <summary>
        /// Faksın id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Faksın başlığı
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Faksın gönderildiği numara
        /// </summary>
        public long did { get; set; }

        /// <summary>
        /// Faksın alıcı sayısı
        /// </summary>
        public int recipient_count { get; set; }

        /// <summary>
        /// Faksın gittiği zaman damgası
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Gönderilen numara bilgileri
        /// </summary>
        public List<Recipient> recipients { get; set; }
    }
}