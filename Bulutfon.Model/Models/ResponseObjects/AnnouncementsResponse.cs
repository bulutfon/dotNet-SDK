using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Ses dosyaları
    /// GET /announcements.json
    /// </summary>
    public class AnnouncementsResponse
    {
        public List<Announcement> announcements { get; set; }
    }
}