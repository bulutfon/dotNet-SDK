using System.Collections.Generic;

namespace Bulutfon.Sdk.Models.ResponseObjects {

    /// <summary>
    /// Ses dosyaları
    /// GET /announcements.json
    /// </summary>
    public class AnnouncementsResponse
    {
        public List<Announcement> announcements { get; set; }
    }
}