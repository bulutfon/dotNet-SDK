using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Ses dosyaları
    /// GET /announcements.json
    /// </summary>
    public class AnnouncementResponse
    {
        public List<Announcement> announcements { get; set; }
    }
}