namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Mesaj Başlığı
    /// </summary>
    public class MessageTitle
    {
        /// <summary>
        /// Mesaj başlığının id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Tanımlı Mesaj Başlığı
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Onay durumu
        /// DRAFT: Taslak (Henüz onaylı değil)
        /// REJECTED: Reddedildi
        /// CONFIRMED: Onaylandı
        /// </summary>
        public string state { get; set; }
    }
}