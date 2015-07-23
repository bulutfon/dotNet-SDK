using System.Collections.Generic;

namespace Bulutfon.Model.Models.ResponseObjects
{
    /// <summary>
    /// Santrale bağlı numarlar
    /// GET /groups.json 
    /// </summary>
    public class GroupsResponse
    {
        public List<Group> groups { get; set; }
    }
}