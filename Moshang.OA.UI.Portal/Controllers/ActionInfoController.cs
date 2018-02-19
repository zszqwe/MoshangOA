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
    public class ActionInfoController : Controller
    {
        short delflagNormal = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;
        public IActionInfoService ActionInfoService { get; set; }
        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        //获取数据
        public ActionResult GetAllActionInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = ActionInfoService.GetPageEntities(pageSize, pageIndex, out total,
                u => u.DelFlag == delflagNormal, u => u.ID, true);

            var tempData =
                temp.Select(
                    a =>
                        new
                        {
                            a.ID,
                            a.IsMenu,
                            a.Url,
                            a.Remark,
                            a.Sort,
                            a.SubTime,
                            a.ModfiedOn,
                            a.HttpMethd,
                            a.MenuIcon,
                            a.ActionName
                        });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //添加
        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.ModfiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.DelFlag = delflagNormal;

            actionInfo.IsMenu = Request["IsMenu"] == "true" ? true : false;

            ActionInfoService.Add(actionInfo);
            return Content("ok");
        }
    }
}