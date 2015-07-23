namespace Bulutfon.Sdk.Models
{
    /// <summary>
    /// Gelen Faks
    /// </summary>
    public class IncomingFax
    {
        /// <summary>
        /// Faksın bezersiz id'si
        /// </summary>
        public string uuid { get; set; }

        /// <summary>
        /// Faksı gönderen
        /// </summary>
        public object sender { get; set; }

        /// <summary>
        /// Faksın geldiği santral numarası
        /// </summary>
        public object receiver { get; set; }

        /// <summary>
        /// Faksın geldiği zaman damgası
        /// </summary>
        public string created_at { get; set; }
    }
}
