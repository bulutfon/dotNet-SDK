using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects {

    /// <summary>
    /// Santralde kullanılan telefon numaraları hakkında bilgiler
    /// GET /dids.json
    /// </summary>
    public class DidsResponse 
    {

        public List<Did> dids { get; set; }
    }
}