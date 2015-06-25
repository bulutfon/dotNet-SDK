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
        public List<Message> messages { get; set; }
    }
}