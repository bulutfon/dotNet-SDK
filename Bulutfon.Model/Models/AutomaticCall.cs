using System.Collections.Generic;

namespace Bulutfon.Model.Models
{
    public class AutomaticCall
    {
        /// <summary>
        /// Otomatik arama id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Otomatik arama başlığı
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Otomatik aramanın başlatıldığı numara
        /// </summary>
        public long did { get; set; }

        /// <summary>
        /// Okunacak dosya adı
        /// </summary>
        public string announcement { get; set; }

        /// <summary>
        /// Kullanıcının bastığı tuşlar kaydedilecek mi?
        /// </summary>
        public bool gather { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Aramaların yapılabileceği günler ve saat aralıkları.
        /// </summary>
        public CallRange call_range { get; set; }

        /// <summary>
        /// Aranan numara bilgileri.
        /// </summary>
        public List<Recipient> recipients { get; set; }
    }
}