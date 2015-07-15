using System.Web;

namespace Mvc4Demo.Models {

    /// <summary>
    /// Faks gönderim formu için model
    /// </summary>
    public class OutgoingFaxForm {

        public string title { get; set; }
        public string receivers { get; set; }
        public long did { get; set; }

        /// <summary>
        /// Faks olarak gönderilecek dosya (file upload)
        /// </summary>
        public HttpPostedFileBase attachment { get; set; }
    }
}