namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Ses dosyası
    /// </summary>
    public class Announcement
    {
        /// <summary>
        /// Ses Dosyası id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Ses Dosyası Adı
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Ses dosyası dosya adı
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// Bekleme müziği olarak seçilip seçilmediği
        /// </summary>
        public bool is_on_hold_music { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public string created_at { get; set; }
    }
}