using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulutfon.Models
{
    /// <summary>
    /// PBX (Santral)
    /// </summary>
    public class Pbx
    {
        /// <summary>
        /// Santral adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Santralin URL adresi
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Santral durumu
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Paket
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// Müşteri türü
        /// </summary>
        public string CustomerType { get; set; }
    }
}