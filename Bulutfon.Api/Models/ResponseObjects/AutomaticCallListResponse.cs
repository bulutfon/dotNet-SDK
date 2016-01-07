using System.Collections.Generic;

namespace Bulutfon.Sdk.Models.ResponseObjects
{
    /// <summary>
    /// Otomatik aramalar
    /// GET /automatic-calls.json
    /// </summary>
    public class AutomaticCallListResponse
    {
        public List<AutomaticCall> automatic_calls { get; set; }
    }
}