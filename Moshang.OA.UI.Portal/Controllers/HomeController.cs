using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (Session["LoginUser"] == null)
            //{
            //    return RedirectToAction("Index","UserLogin");
            //}
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
    }
}