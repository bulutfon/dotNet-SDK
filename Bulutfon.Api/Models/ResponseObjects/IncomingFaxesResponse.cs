using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Gelen fakslar
    /// GET /incoming-faxes.json
    /// </summary>
    public class IncomingFaxesResponse
    {
        public List<IncomingFax> incoming_faxes { get; set; }
    }
}