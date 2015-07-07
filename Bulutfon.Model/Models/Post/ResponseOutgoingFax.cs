using System.Collections.Generic;

namespace Bulutfon.Model.Models.Post {

    public class ResponseFaxRecipient {
        public long number { get; set; }
        public string state { get; set; }
    }

    public class ResponseOutgoingFax {
        public string id { get; set; }
        public string title { get; set; }
        public long did { get; set; }
        public int recipient_count { get; set; }
        public string create_at { get; set; }
        public List<ResponseFaxRecipient> recipient { get; set; }
    }
}