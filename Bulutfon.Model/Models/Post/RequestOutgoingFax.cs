namespace Bulutfon.Model.Models.Post {

    public class RequestOutgoingFax {
        public string title { get; set; }
        public string receivers { get; set; }
        public long did { get; set; }
        public string attachment { get; set; }
    }
}