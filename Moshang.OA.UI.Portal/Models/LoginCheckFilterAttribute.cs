using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moshang.OA.UI.Portal.Models
{
    public class LoginCheckFilterAttribute : ActionFilterAttribute
    {
        public bool IsChecked { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Session["LoginUser"] == null&&IsChecked)
            {
                filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
            }
        }
    }
}