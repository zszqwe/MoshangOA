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
        public bool IsCheckUserLogin = true;
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);


            //TODO: 暂时去掉验证
            return;

            if (IsCheckUserLogin)
            {
                //Memchache+Cookie方式
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = (UserInfo)Common.Cache.CacheHelper.GetCache
                    (userGuid);//as UserInfo
                if (userInfo == null)
                {
                    //长时间为操作 缓存已超时
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                LoginUser = userInfo;
                //滑动窗口机制 (响应后刷新缓冲时间)
                Common.Cache.CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));


                //Session方式
                //if (filterContext.HttpContext.Session["LoginUser"] == null && IsCheckedUserLogin)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser= filterContext.HttpContext.Session["LoginUser"] as UserInfo;

                //} 
            }



        }
    }
}