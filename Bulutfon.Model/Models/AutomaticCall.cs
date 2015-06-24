using System.Collections.Generic;

namespace Bulutfon.Model.Models
{
    public class AutomaticCall
    {
        public int id { get; set; }
        public string title { get; set; }
        public long did { get; set; }
        public string announcement { get; set; }
        public bool gather { get; set; }
        public string created_at { get; set; }
        public CallRange call_range { get; set; }
        public List<Recipient> recipients { get; set; }
    }
}