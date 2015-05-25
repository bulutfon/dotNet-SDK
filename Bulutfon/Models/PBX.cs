namespace Bulutfon.Models
{
    /// <summary>
    /// PBX (Santral)
    /// </summary>
    public class Pbx
    {
        /// <summary>
        /// Santral adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Santralin URL adresi
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Santral durumu,
        /// DRAFT: Taslak
        /// RECANTED: Vazgeçildi
        /// CONFIRMED: Onaylandı
        /// CANCEL: İptal Edildi
        /// SUSPENDED: Gelen aramalara kapalı
        /// PRETERMINATED: Bütün aramalara kapalı
        /// TERMINATED: Borçları nedeniyle kapatıldı
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Paket,
        /// SMALL: Giriş Paketi
        /// MIDDLE: Orta Paket
        /// LARGE: Büyük Paket
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// Müşteri türü,
        /// INVIDUAL: Bireysel Müşteri
        /// CORPORATE: Kurumsal Müşteri
        /// </summary>
        public string CustomerType { get; set; }
    }
}