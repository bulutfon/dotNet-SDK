using System.Collections.Generic;

namespace Bulutfon.Sdk.Models.ResponseObjects
{
    /// <summary>
    /// Bulutfon ile gönderilen fakslar
    /// GET /outgoing-faxes.json
    /// </summary>
    public class OutgoingFaxesResponse
    {
        /// <summary>
        /// Fakslar
        /// </summary>
        public List<Fax> faxes { get; set; }
    }
}