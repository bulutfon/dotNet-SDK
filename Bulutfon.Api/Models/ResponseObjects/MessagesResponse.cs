using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Mesaj listesi
    /// GET /messages.json
    /// </summary>
    public class MessagesResponse
    {
        /// <summary>
        /// Mesajlar
        /// </summary>
        public List<Bulutfon.Api.Models.Message> messages { get; set; }
    }
}