using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.Common;
using Moshang.OA.IBLL;
using Moshang.OA.UI.Portal.Models;

namespace Moshang.OA.UI.Portal.Controllers
{
    [LoginCheckFilter(IsChecked =false)]
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        public IUserInfoService UserInfoService { get; set; }

        #region 验证码
        public ActionResult GetCodeImage()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);
            //验证码放到Session中
            Session["VCode"] = strCode;

            byte[] imBytes = validateCode.CreateValidateGraphic(strCode);
            return File(imBytes, @"image/jpeg");
        }
        #endregion



        #region 处理登陆表单
        public ActionResult ProcessLogin()
        {
            //验证验证码
            string strCode = Request["vCode"];
            string sessionCode = Session["VCode"] as string;
            Session["VCode"] = null;
            if (string.IsNullOrEmpty(sessionCode)|| strCode != sessionCode)
            {
                return Content("验证码错误");
            }

            //校验用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
            short delNormal = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;
            var userInfos=UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal).FirstOrDefault();

            if (userInfos == null)
            {
                //没有查出数据
                return Content("用户名或密码错误");
            }
            else
            {
                Session["LoginUser"] = userInfos;
                return Content("登陆成功");
            }
        }
        #endregion
    }
}