using System.Collections.Generic;

namespace Bulutfon.Sdk.Models.ResponseObjects
{
    /// <summary>
    /// Dahili numaralar
    /// GET /extensions.json
    /// </summary>
    public class ExtensionsResponse
    {
        public List<Extension> extensions { get; set; }
    }
}