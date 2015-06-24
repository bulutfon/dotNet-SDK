namespace Bulutfon.Model.Models
{
    public class Pagination
    {
        public int page { get; set; }
        public int total_count { get; set; }
        public int total_pages { get; set; }
        public int limit { get; set; }
        public object previous_page { get; set; }
        public string next_page { get; set; }
    }
}