using System.Collections.Generic;

namespace Bulutfon.Sdk.Models
{
    public class Cdr
    {
        public string uuid { get; set; }
        public string bf_calltype { get; set; }
        public string direction { get; set; }
        public long caller { get; set; }
        public long callee { get; set; }
        public string call_time { get; set; }
        public string answer_time { get; set; }
        public string hangup_time { get; set; }
        public string call_record { get; set; }
        public string hangup_cause { get; set; }
        public string hangup_state { get; set; }
        public List<CallFlow> call_flow { get; set; }
    }
}