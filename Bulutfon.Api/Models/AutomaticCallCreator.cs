using System.Collections.Generic;

namespace Bulutfon.Sdk.Models
{
    public class AutomaticCallCreator
    {
        /// <summary>
        /// Otomatik arama başlığı
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Otomatik aramanın başlatıldığı numara
        /// </summary>
        public long did { get; set; }

        /// <summary>
        /// Okunacak dosya idsi
        /// </summary>
        public int announcement_id { get; set; }

        /// <summary>
        /// Aranacak numaralar virgül ile ayrılmış şekilde
        /// </summary>
        public string receivers { get; set; }

        /// <summary>
        /// Aranacak numaralar virgül ile ayrılmış şekilde
        /// </summary>
        public bool gather { get; set; }

        /// <summary>
        /// Arama saatleri aktif mi?
        /// </summary>
        public bool hours_active { get; set; }

        /// <summary>
        /// Pazartesi aramabilir mi?
        /// </summary>
        public bool mon_active { get; set; }

        /// <summary>
        /// Pazartesi başlama saati
        /// </summary>
        public string mon_start { get; set; }

        /// <summary>
        /// Pazartesi bitiş saati
        /// </summary>
        public string mon_finish { get; set; }

        /// <summary>
        /// Salı aramabilir mi?
        /// </summary>
        public bool tue_active { get; set; }

        /// <summary>
        /// Salı başlama saati
        /// </summary>
        public string tue_start { get; set; }

        /// <summary>
        /// Salı bitiş saati
        /// </summary>
        public string tue_finish { get; set; }


        /// <summary>
        /// Çarşamba aramabilir mi?
        /// </summary>
        public bool wed_active { get; set; }

        /// <summary>
        /// Çarşamba başlama saati
        /// </summary>
        public string wed_start { get; set; }

        /// <summary>
        /// Çarşamba bitiş saati
        /// </summary>
        public string wed_finish { get; set; }

        /// <summary>
        /// Perşembe aramabilir mi?
        /// </summary>
        public bool thu_active { get; set; }

        /// <summary>
        /// Perşembe başlama saati
        /// </summary>
        public string thu_start { get; set; }

        /// <summary>
        /// Perşembe bitiş saati
        /// </summary>
        public string thu_finish { get; set; }

        /// <summary>
        /// Cuma aramabilir mi?
        /// </summary>
        public bool fri_active { get; set; }

        /// <summary>
        /// Cuma başlama saati
        /// </summary>
        public string fri_start { get; set; }

        /// <summary>
        /// Cuma bitiş saati
        /// </summary>
        public string fri_finish { get; set; }

        /// <summary>
        /// Cumartesi aramabilir mi?
        /// </summary>
        public bool sat_active { get; set; }

        /// <summary>
        /// Cumartesi başlama saati
        /// </summary>
        public string sat_start { get; set; }

        /// <summary>
        /// Cumartesi bitiş saati
        /// </summary>
        public string sat_finish { get; set; }

        /// <summary>
        /// Pazar aramabilir mi?
        /// </summary>
        public bool sun_active { get; set; }

        /// <summary>
        /// Pazar başlama saati
        /// </summary>
        public string sun_start { get; set; }

        /// <summary>
        /// Pazar bitiş saati
        /// </summary>
        public string sun_finish { get; set; }

        public AutomaticCallCreator()
        {
            gather = true;
            hours_active = false;
            mon_active = true;
            mon_start = "09:00";
            mon_finish = "18:00";

            tue_active = true;
            tue_start = "09:00";
            tue_finish = "18:00";

            wed_active = true;
            wed_start = "09:00";
            wed_finish = "18:00";

            thu_active = true;
            thu_start = "09:00";
            thu_finish = "18:00";

            fri_active = true;
            fri_start = "09:00";
            fri_finish = "18:00";

            sat_active = true;
            sat_start = "09:00";
            sat_finish = "18:00";

            sun_active = true;
            sun_start = "09:00";
            sun_finish = "18:00";
        }
    }
}
