using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bulutfon.Model.Models.Post;
using Bulutfon.MVC4.Api;
using Mvc4Demo.Models;

namespace Mvc4Demo.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Did(string id) {
            var did = BulutfonApi.GetDid((TokenProvider)Session[BulutfonApi.TokenProviderKey], id);
            return View(did);
        }

        [Authorize]
        public ActionResult Dids() {
            var dids = BulutfonApi.GetDids((TokenProvider)Session[BulutfonApi.TokenProviderKey]);
            return View(dids);
        }

        [Authorize]
        public ActionResult IncomingFaxes() {
            var faxes = BulutfonApi.GetIncomingFaxes((TokenProvider)Session[BulutfonApi.TokenProviderKey]);
            return View(faxes);
        }

        [Authorize]
        public ActionResult DownloadFax(string id) {
            return BulutfonApi.DownloadIncomingFaxAsTiff((TokenProvider)Session[BulutfonApi.TokenProviderKey], id);
        }

        [Authorize]
        public ActionResult OutgoingFaxes() {
            var faxes = BulutfonApi.GetFaxes((TokenProvider)Session[BulutfonApi.TokenProviderKey]);
            return View(faxes);
        }

        [Authorize]
        public ActionResult OutgoingFax(string id) {
            var fax = BulutfonApi.GetFax((TokenProvider)Session[BulutfonApi.TokenProviderKey], id);
            return View(fax);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadFax() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadFax(OutgoingFaxForm outgoingFax) {
            if (outgoingFax.attachment != null && outgoingFax.attachment.ContentLength > 0) {
                /*var ret =*/ BulutfonApi.SendFax((TokenProvider)Session[BulutfonApi.TokenProviderKey], 
                    outgoingFax.attachment, // faks dosyası
                    outgoingFax.receivers, // alıcılar
                    outgoingFax.did, // gönderen numara
                    outgoingFax.title); // başlık
                // TODO: ret nesnesi üzerinden her bir alıcıya ait gönderme durumu raporlanabilir
            }
            return RedirectToAction("OutgoingFaxes");
        }

        public ActionResult Index() {
            ViewBag.Message = "Bulutfon .Net-SDK Demo Uygulamasına Hoşgeldiniz!";
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Telif Hakkı (c) 2015, Bulutfon A.Ş.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
