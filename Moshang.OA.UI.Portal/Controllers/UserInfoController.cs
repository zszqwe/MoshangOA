﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.BLL;
using Moshang.OA.IBLL;
using Moshang.OA.Model;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        //UserInfoService UserInfoService =new UserInfoService();
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            ViewData.Model = UserInfoService.GetEntities(u => true);

            return View();
        }

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
    }
}