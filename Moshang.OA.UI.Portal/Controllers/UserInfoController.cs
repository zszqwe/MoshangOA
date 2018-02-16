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

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserInfos()
        {
            //JQuery easyUI:table:{total:32,row:[{},{}]}

            //初始化自动发送一下两个参数
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            short delflagNormal = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;

            //获取当前页数据
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex, out total,
                                                                u => u.DelFlag == delflagNormal, u => u.ID, true).Select(
                u => new
                {
                    u.ID,
                    u.UName,
                    u.Remark,
                    u.ShowName,
                    u.SubTime,
                    u.ModfiedOn,
                    u.Pwd
                }

                );
            //序列化实体注意导航属性依赖
            var data = new { total = total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //添加
        public ActionResult Add(UserInfo userinfo)
        {
            userinfo.ModfiedOn = DateTime.Now;
            userinfo.SubTime = DateTime.Now;
            userinfo.DelFlag = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userinfo);

            return Content("OK");
        }

        //修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u=> u.ID==id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("OK");
        }
        //删除
        public ActionResult Delete(string ids)
        {
            //判断是否为空
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            string[] strids = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strids)
            {
                idList.Add(int.Parse(strId));
            }
            UserInfoService.DeleteListByLogical(idList);

            return Content("OK");
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