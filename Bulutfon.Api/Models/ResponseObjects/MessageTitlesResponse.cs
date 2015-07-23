using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Panelden tanımlanmış mesaj başlıkları
    /// GET /message-titles.json?access_token=xxx*
    /// </summary>
    public class MessageTitlesResponse
    {
        public List<MessageTitle> message_titles { get; set; }
    }
}