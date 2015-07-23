namespace Bulutfon.Sdk.Models
{
    /// <summary>
    /// Satral Numarası çalışma saatleri
    /// </summary>
    public class DidWorkDay
    {
        /// <summary>
        /// Mesai saatleri içinde mi?
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// Mesai başlangıcı
        /// </summary>
        public string shift_start { get; set; }

        /// <summary>
        /// Öğle arası başlangıcı
        /// </summary>
        public string lunch_break_start { get; set; }

        /// <summary>
        /// Öğle arası bitişi
        /// </summary>
        public string lunch_break_finish { get; set; }

        /// <summary>
        /// Mesai bitişi
        /// </summary>
        public string shift_finish { get; set; }
    }
}