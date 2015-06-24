namespace Bulutfon.Model.Models
{
    /// <summary>
    /// Bulutfon kullanıcı bilgisi
    /// GET /me.json
    /// </summary>
    public class BulutfonUser
    {
        public User user { get; set; }
        public Pbx pbx { get; set; }
        public Credit credit { get; set; }
    }
}