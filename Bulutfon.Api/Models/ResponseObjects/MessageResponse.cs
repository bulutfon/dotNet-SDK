namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Mesaj Bilgileri
    /// GET /messages/id.json
    /// </summary>
    public class MessageResponse
    {
        public Bulutfon.Api.Models.Message message { get; set; }
    }
}