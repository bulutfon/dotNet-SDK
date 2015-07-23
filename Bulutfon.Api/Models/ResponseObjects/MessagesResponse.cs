using System.Collections.Generic;

namespace Bulutfon.Sdk.Models.ResponseObjects
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
        public List<Bulutfon.Sdk.Models.Message> messages { get; set; }
    }
}