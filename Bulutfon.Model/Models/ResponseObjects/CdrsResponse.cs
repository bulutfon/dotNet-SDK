using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    public class CdrsResponse
    {
        public List<Cdr> cdrs { get; set; }
        public Pagination pagination { get; set; }
    }
}