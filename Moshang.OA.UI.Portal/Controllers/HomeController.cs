using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.BLL;
using Moshang.OA.IBLL;
using Moshang.OA.Model;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        short delflagNormal = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;

        public ActionResult Index()
        {
            ViewBag.AllMenu = LoadUserMenu();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoadUserMenu()//List<ActionInfo>
        {
            //获取当前用户
            var userId = this.LoginUser.ID;
            var user = UserInfoService.GetEntities(u => u.ID == userId).FirstOrDefault();

            //获取当前用户所有菜单类型权限
            var allRole = user.RoleInfo;
            var allRoleAction = (from r in allRole
                                 from a in r.ActionInfo
                                 select a.ID).ToList();

            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoID).ToList();
            var allActionIds = allDenyActionIds.Where(u => !allRoleAction.Contains(u)).ToList();//from a in allRoleActions where !allDenyActionIds select a

            var allUserActionIds = (from t in user.R_UserInfo_ActionInfo
                                    where t.HasPermission == true
                                    select t.ActionInfoID).ToList();

            allActionIds.AddRange(allUserActionIds);
            allActionIds = allActionIds.Distinct().ToList();

            var actionList = ActionInfoService.GetEntities(a => allUserActionIds.Contains(a.ID) && a.IsMenu == true && a.DelFlag == delflagNormal).ToList();

            var data = from a in actionList
                       select new { icon = a.MenuIcon, title = a.ActionName, url = a.Url };
            return Json(data, JsonRequestBehavior.AllowGet);
            //return actionList;

        }
    }
}