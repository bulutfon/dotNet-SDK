using System.Collections.Generic;

namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Grup
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Grup id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Grup Numarası
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// Grup adı
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Grup zaman aşımı
        /// </summary>
        public int timeout { get; set; }

        /// <summary>
        /// Bekleme süresi
        /// </summary>
        public int delay_time { get; set; }

        /// <summary>
        /// Zil süresi
        /// </summary>
        public int ring_duration { get; set; }

        /// <summary>
        /// Gruba ait dahililer
        /// </summary>
        public List<Extension> extensions { get; set; }
    }
}