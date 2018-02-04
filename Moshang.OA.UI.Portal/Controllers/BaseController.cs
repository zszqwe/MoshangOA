using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.Model;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base 在当前控制器中所有方法执行前执行
        public bool IsCheckedUserLogin = true;
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Session["LoginUser"] == null && IsCheckedUserLogin)
            {
                filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
            }
            else
            {
                LoginUser= filterContext.HttpContext.Session["LoginUser"] as UserInfo;
                
            }
        }
    }
}