namespace Bulutfon.Model.Models.Post {

    public class RequestSendMessage {
        public string title { get; set; }
        public string receivers { get; set; }
        public string content { get; set; }
        public bool is_single_sms { get; set; }
        public bool is_future_sms { get; set; }
        public string send_date { get; set; }
    }
}