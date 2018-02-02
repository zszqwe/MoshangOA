using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moshang.OA.UI.Portal.Models
{
    public class MyExceptionFilterAttribut:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //将错误信息写到日志文件里面去
            Common.LogHelper.WriteLog(filterContext.Exception.ToString());
        }

    }
}