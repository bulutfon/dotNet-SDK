namespace Bulutfon.Sdk.Models
{
    public class Recipient
    {
        /// <summary>
        /// Aranan numara
        /// </summary>
        public object number { get; set; }

        /// <summary>
        /// Çağrı başarılı mı?
        /// </summary>
        public bool has_called { get; set; }

        /// <summary>
        /// Aranan kişi bir tuşa bastıysa basılan tuş
        /// </summary>
        public int gather { get; set; }
    }
}