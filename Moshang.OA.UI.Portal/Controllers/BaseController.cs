using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.Common.Cache;
using Moshang.OA.IBLL;
using Moshang.OA.Model;
using Spring.Context;
using Spring.Context.Support;

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


            if (IsCheckUserLogin)
            {


                #region 用户登陆校验
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

                #region Session方式

                //if (filterContext.HttpContext.Session["LoginUser"] == null && IsCheckedUserLogin)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser= filterContext.HttpContext.Session["LoginUser"] as UserInfo;

                //}  
                #endregion
                #endregion

                #region 用户权限校验

                //校验权限
                //获取当前请求对应的权限数据
                if (LoginUser.UName == "Moshang")
                {
                    return;//Moshang`s backdoor
                }

                string url = Request.Url.AbsolutePath;
                string[] splitArr = url.Split('/');
                string newStrurl = splitArr[0]+"/" + splitArr[1] + "/" + splitArr[2];
                string httpMethod = Request.HttpMethod.ToLower();

                //通过容器获取
                IApplicationContext ctx = ContextRegistry.GetContext();
                //ctx.GetObject("CacheHelper");
                IActionInfoService actionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;

                IR_UserInfo_ActionInfoService rUserInfoActionInfoService = ctx.GetObject("R_UserInfo_ActionInfoService") as IR_UserInfo_ActionInfoService;

                IUserInfoService UserInfoService =
                    ctx.GetObject("UserInfoService") as IUserInfoService;

                var actionInfo =
                actionInfoService.GetEntities(a => a.Url.ToLower()== newStrurl && a.HttpMethd.ToLower() == httpMethod).FirstOrDefault();

                if (actionInfo == null)
                {
                    Response.Redirect("/Error.html");
                }

                var rUAs = rUserInfoActionInfoService.GetEntities(u => u.UserInfoID == LoginUser.ID);

                var item = (from a in rUAs
                    where a.ActionInfoID == actionInfo.ID
                    select a).FirstOrDefault();
                if (item != null)
                {
                    if (item.HasPermission == true)
                    {
                        return;
                    }
                    else
                    {
                        Response.Redirect("/Error.html");
                    }
                }

                //
                var user = UserInfoService.GetEntities(u => u.ID == LoginUser.ID).FirstOrDefault();

                var allRoles = from r in user.RoleInfo
                    select r;
                var actions = from r in allRoles
                    from a in r.ActionInfo
                    select a;
                var temp = (from a in actions
                    where a.ID == actionInfo.ID
                    select a).Count();
                if (temp <= 0)
                {
                    Response.Redirect("/Error.html");
                }

                #endregion


            }




        }
    }
}