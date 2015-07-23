using System.Collections.Generic;

namespace Bulutfon.Sdk.Models.Post {

    public class Recepient {
        public string number { get; set; }
        public string state { get; set; }
    }

    public class ResponseSendMessage {
        public string title { get; set; }
        public string content { get; set; }
        public bool sent_as_single_sms { get; set; }
        public bool is_planned_sms { get; set; }
        public string send_date { get; set; }
        public string created_at { get; set; }
        public List<Recepient> recepients { get; set; }
    }
}