using System.Collections.Generic;
using System.Net;
using Bulutfon.Model.Models;
using Newtonsoft.Json;

namespace Bulutfon.MVC4.Api {

    public static class BulutfonApi {

        public const string Endpoint = "https://api.bulutfon.com/";

        public static T GetObject<T>(string uri, string token, string key = "") where T : class {
            const string tokenKey = "?access_token=";
            using (WebClient client = new WebClient()) {
                var keyValue = string.Empty;
                if (!string.IsNullOrEmpty(key))
                    keyValue = string.Format("/{0}", keyValue);
                string str = client.DownloadString(uri + keyValue + tokenKey + token);
                if (string.IsNullOrEmpty(str)) {
                    return null;
                }
                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        public static List<Announcement> GetAnnouncements(string token) {
            return GetObject<AnnouncementsResponse>("https://api.bulutfon.com/dids", token).announcements;
        }
    }
}