using System.Web;
using System.Web.Mvc;

namespace Bulutfon.MVC4.Test
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}