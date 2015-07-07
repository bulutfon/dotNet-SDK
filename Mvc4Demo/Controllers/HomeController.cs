﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bulutfon.MVC4.Api;

namespace Mvc4Demo.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Did(string id) {
            var did = BulutfonApi.GetDid(Session["token"].ToString(), id);
            return View(did);
        }

        [Authorize]
        public ActionResult Dids() {
            var dids = BulutfonApi.GetDids(Session["token"].ToString());
            return View(dids);
        }

        [Authorize]
        public ActionResult IncomingFaxes() {
            var faxes = BulutfonApi.GetIncomingFaxes(Session["token"].ToString());
            return View(faxes);
        }

        [Authorize]
        public ActionResult DownloadFax(string id) {
            return BulutfonApi.DownloadIncomingFaxAsTiff(Session["token"].ToString(), id);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
