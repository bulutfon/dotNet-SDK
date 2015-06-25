namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Mesaj Bilgileri
    /// GET /messages/id.json
    /// </summary>
    public class MessageResponse
    {
        public Message message { get; set; }
    }
}