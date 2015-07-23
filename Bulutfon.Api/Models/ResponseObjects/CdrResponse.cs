namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Santrale ait arama kaydının detayı
    /// GET /cdrs/uuid.json
    /// </summary>
    public class CdrResponse
    {
        public Cdr cdr { get; set; }
    }
}