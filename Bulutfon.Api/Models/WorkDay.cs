namespace Bulutfon.Model.Models
{
    public class WorkDay
    {
        /// <summary>
        /// O gün aranabilir mi?
        /// </summary>
        public bool active { get; set; }

        /// <summary>
        /// Aramaya başlama saati
        /// </summary>
        public string start { get; set; }

        /// <summary>
        /// Arama bitiş saati
        /// </summary>
        public string finish { get; set; }
    }
}