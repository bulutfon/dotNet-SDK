namespace Bulutfon.Sdk.Models.ResponseObjects
{
    /// <summary>
    /// Bulutfon kullanıcı bilgisi
    /// GET /me.json
    /// </summary>
    public class MeResponse
    {
        public User user { get; set; }
        public Pbx pbx { get; set; }
        public Credit credit { get; set; }
    }
}