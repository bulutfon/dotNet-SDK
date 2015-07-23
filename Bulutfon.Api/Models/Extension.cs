using System.Collections.Generic;

namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Dahili hakkında bilgiler
    /// GET /extensions/:id.json
    /// </summary>
    public class Extension
    {
        /// <summary>
        /// Dahilinin id'si
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Dahilinin Numarası
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// Dahilinin bir cihaz ile giriş yapıp yapmadığı
        /// </summary>
        public bool registered { get; set; }

        /// <summary>
        /// Dahiliyi kullanan kişinin adı soyadı
        /// </summary>
        public string caller_name { get; set; }

        /// <summary>
        /// Dahiliyi kullanan kişinin email adresi
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Dahilinin bağlı bulunduğu santral numarası
        /// </summary>
        public string did { get; set; }

        /// <summary>
        /// Sesli mesaj aktif mi?
        /// </summary>
        public bool voice_mail { get; set; }

        /// <summary>
        /// Varsa yönlendirme koşulu
        /// NONE: Hiçbir zaman
        /// UNREACHABLE: Ulaşılamadığında
        /// ALWAYS: Her zaman
        /// </summary>
        public string redirection_type { get; set; }

        /// <summary>
        /// Varsa yönlendirme tipi
        /// Auto attendant: Menü
        /// Group: Grup
        /// Extension: Dahili
        /// </summary>
        public string destination_type { get; set; }

        /// <summary>
        /// Varsa yönlendirme numarası
        /// </summary>
        public int destination_number { get; set; }

        /// <summary>
        /// Varsa yönlendirilecek dış numara
        /// </summary>
        public object external_number { get; set; }

        /// <summary>
        /// Dahilinin arama yetkileri
        /// </summary>
        public List<string> acl { get; set; }
    }
}