using System.Collections.Generic;

namespace Bulutfon.Sdk.Models
{
    public class CallFlow
    {
        public long callee { get; set; }
        public string start_time { get; set; }
        public string answer_time { get; set; }
        public string hangup_time { get; set; }
        public string redirection { get; set; }
        public int redirection_target { get; set; }
        public List<Origination> origination { get; set; }
    }
}