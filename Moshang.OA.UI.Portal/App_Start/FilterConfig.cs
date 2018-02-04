using System.Web;
using System.Web.Mvc;
using Moshang.OA.UI.Portal.Models;

namespace Moshang.OA.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new LoginCheckFilterAttribute() { IsChecked = true })
            ;
        }
    }
}
