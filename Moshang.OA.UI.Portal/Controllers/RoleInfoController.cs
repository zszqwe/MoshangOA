using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.IBLL;
using Moshang.OA.Model;
using Moshang.OA.Model.Param;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class RoleInfoController : BaseController
    {
        public IRoleInfoService RoleInfoService { get; set; }
        private short delflagNormal = (short) Moshang.OA.Model.Enum.DelFlagEnum.Normal;

        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }

        //获取数据
        public ActionResult GetAllRoleInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = RoleInfoService.GetPageEntities(pageSize, pageIndex, out total,
                r => r.DelFlag == delflagNormal, r => r.ID, true);

            var tempData =
                temp.Select(
                    r =>
                        new
                        {
                            r.ID,
                            r.RoleName,
                            r.Remark,
                            r.SubTime,
                            r.ModfiedOn,
                            r.DelFlag

                        });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //添加
        public ActionResult Add(RoleInfo rolenInfo)
        {
            rolenInfo.ModfiedOn = DateTime.Now;
            rolenInfo.SubTime = DateTime.Now;
            rolenInfo.DelFlag = delflagNormal;
            
            RoleInfoService.Add(rolenInfo);
            return Content("ok");
        }
    }
}