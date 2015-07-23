namespace Bulutfon.Sdk.Models
{
    /// <summary>
    /// Santral (PBX)
    /// </summary>
    public class Pbx
    {
        /// <summary>
        /// Santral adı
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Santral URL'i
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Santral durumu
        /// DRAFT: Taslak
        /// RECANTED: Vazgeçildi
        /// CONFIRMED: Onaylandı
        /// CANCEL: İptal Edildi
        /// SUSPENDED: Gelen aramalara kapalı
        /// PRETERMINATED: Bütün aramalara kapalı
        /// TERMINATED: Borçları nedeniyle kapatıldı
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Santral paketi
        /// SMALL: Giriş Paketi
        /// MIDDLE: Orta Paket
        /// LARGE: Büyük Paket
        /// </summary>
        public string package { get; set; }

        /// <summary>
        /// Müşteri tipi
        /// INVIDUAL: Bireysel Müşteri
        /// CORPORATE: Kurumsal Müşteri
        /// </summary>
        public string customer_type { get; set; }
    }
}