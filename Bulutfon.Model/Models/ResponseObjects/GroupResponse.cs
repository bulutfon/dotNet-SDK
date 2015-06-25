namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Numaraya ait detayları ve varsa mesai saatlerini gösterir
    /// GET /groups/:id.json 
    /// </summary>
    public class GroupResponse
    {
        public Group group { get; set; }
    }
}
