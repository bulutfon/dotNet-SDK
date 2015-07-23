namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Santralde kullanılan telefon numaraları hakkında bilgiler
    /// GET /dids/:id.json
    /// </summary>
    public class DidResponse
    {
        public Did did { get; set; }
    }
}