using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.IBLL;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class OrderInfoController : BaseController
    {
        // GET: OrderInfo
        public IOrderInfoService OrderInfoService { get; set; }

        public ActionResult Index()
        {
            ViewData.Model = OrderInfoService.GetEntities(u => true).ToList();
            return View();
        }
    }
}