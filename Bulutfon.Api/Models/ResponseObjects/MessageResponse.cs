namespace Bulutfon.Sdk.Models.ResponseObjects
{
    /// <summary>
    /// Mesaj Bilgileri
    /// GET /messages/id.json
    /// </summary>
    public class MessageResponse
    {
        public Bulutfon.Sdk.Models.Message message { get; set; }
    }
}