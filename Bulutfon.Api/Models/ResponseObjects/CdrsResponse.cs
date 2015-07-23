using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Santrale ait arama kayıtları
    /// GET /cdrs.json 
    /// </summary>
    public class CdrsResponse
    {
        public List<Cdr> cdrs { get; set; }
        public Pagination pagination { get; set; }
    }
}