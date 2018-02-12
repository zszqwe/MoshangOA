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
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        //UserInfoService UserInfoService =new UserInfoService();
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            ViewData.Model = UserInfoService.GetEntities(u => true);

            return View();
        }

        public ActionResult GetAllUserInfos()
        {
            //JQuery easyUI:table:{total:32,row:[{},{}]}

            //初始化自动发送一下两个参数
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "0");
            int total = 0;

            short delflagNormal = (short)Moshang.OA.Model.Enum.DelFlagEnum.Deleted;

            //获取当前页数据
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delflagNormal, u => u.ID,
                 true).Select(
                u =>
                new { u.ID, u.UName, u.Pwd, u.Remark, u.ShowName, u.SubTime, u.ModfiedOn });//

            //序列化实体注意导航属性依赖
            var data = new { total = total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}