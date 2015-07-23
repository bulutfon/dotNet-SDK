namespace Bulutfon.Sdk.Models
{
    public class Did
    {
        /// <summary>
        /// Santral Numarasının id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Santral Numarası
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// Santral Numarasının durumu
        /// DRAFT: Taslak
        /// IN-PROGRESS: İşleme alındı
        /// CONFIRMED: Onaylandı
        /// CANCELED: Vazgeçildi
        /// TERMINATED: Kapatıldı
        /// NTS-DRAFT: NTS ile taşıma taslak
        /// NTS-IN-PROGRESS: NTS işleniyor
        /// NTS-CANCEL: NTS vazgeçildi
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Santral Numarasının yönlendirileceği tip (Menü, grup, dahili)
        /// Auto attendant: Menü
        /// Group: Grup
        /// Extension: Dahili
        /// </summary>
        public string destination_type { get; set; }

        /// <summary>
        /// Santral Numarasının yönlendirileceği id
        /// </summary>
        public int destination_id { get; set; }

        /// <summary>
        /// Santral Numarasının yönlendirileceği numara
        /// </summary>
        public string destination_number { get; set; }

        /// <summary>
        /// Mesai saatlerinin aktif olup olmadığı
        /// </summary>
        public bool working_hour { get; set; }

        /// <summary>
        /// Çalışma saatleri
        /// </summary>
        public WorkingHours working_hours { get; set; }
    }
}