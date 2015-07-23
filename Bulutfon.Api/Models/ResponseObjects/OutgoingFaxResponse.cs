namespace Bulutfon.Sdk.Models.ResponseObjects
{
    /// <summary>
    /// Gönderilen Faks
    /// GET /outgoing-faxes/uuid.json
    /// </summary>
    public class OutgoingFaxResponse
    {
        /// <summary>
        /// Faks
        /// </summary>
        public Fax fax { get; set; }
    }
}