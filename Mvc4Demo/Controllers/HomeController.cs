using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bulutfon.Sdk;
using Bulutfon.Sdk.Models.Post;
using Bulutfon.OAuth;
using Mvc4Demo.Models;

namespace Mvc4Demo.Controllers {

    public class HomeController : Controller {

        [Authorize]
        public ActionResult Messages() {
            var messages = Bulutfon.Sdk.BulutfonApi.GetMessages((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey]);
            return View(messages);
        }

        public ActionResult Message(string id) {
            var message = Bulutfon.Sdk.BulutfonApi.GetMessage((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey], id);
            return View(message);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SendSms() {
            return View();
        }

        public ActionResult SendSms(Bulutfon.Sdk.Models.Post.RequestSendMessage message) {
            //TODO
            return null;
        }

        [Authorize]
        public ActionResult Did(string id) {
            var did = Bulutfon.Sdk.BulutfonApi.GetDid((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey], id);
            return View(did);
        }

        [Authorize]
        public ActionResult Dids() {
            var dids = Bulutfon.Sdk.BulutfonApi.GetDids((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey]);
            return View(dids);
        }

        [Authorize]
        public ActionResult IncomingFaxes() {
            var faxes = Bulutfon.Sdk.BulutfonApi.GetIncomingFaxes((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey]);
            return View(faxes);
        }

        //[Authorize]
        //public ActionResult DownloadFax(string id) {
        //    return BulutfonApi.DownloadIncomingFaxAsTiff((TokenProvider)Session[BulutfonApi.TokenProviderKey], id);
        //}

        [Authorize]
        public ActionResult OutgoingFaxes() {
            var faxes = Bulutfon.Sdk.BulutfonApi.GetFaxes((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey]);
            return View(faxes);
        }

        [Authorize]
        public ActionResult OutgoingFax(string id) {
            var fax = Bulutfon.Sdk.BulutfonApi.GetFax((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey], id);
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
                /*var ret =*/ Bulutfon.Sdk.BulutfonApi.SendFax((Token)Session[Bulutfon.Sdk.BulutfonApi.TokenProviderKey], 
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