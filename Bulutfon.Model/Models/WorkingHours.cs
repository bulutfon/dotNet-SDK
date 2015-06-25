namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Santral Numarasının çalışma saatleri
    /// </summary>
    public class WorkingHours
    {
        public DidWorkDay monday { get; set; }
        public DidWorkDay tuesday { get; set; }
        public DidWorkDay wednesday { get; set; }
        public DidWorkDay thursday { get; set; }
        public DidWorkDay friday { get; set; }
        public DidWorkDay saturday { get; set; }
        public DidWorkDay sunday { get; set; }
    }
}