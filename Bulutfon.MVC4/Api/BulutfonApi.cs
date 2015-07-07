using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BitMiracle.LibTiff.Classic;
using Bulutfon.Model.Models;
using Bulutfon.Model.Models.Post;
using Bulutfon.Model.Models.ResponseObjects;
using Newtonsoft.Json;

namespace Bulutfon.MVC4.Api {

    public static class BulutfonApi {

        public const string Endpoint = "https://api.bulutfon.com/";

        public static T GetObject<T>(string uri, string token, string key = "") where T : class {
            const string tokenKey = "?access_token=";
            using (WebClient client = new WebClient()) {
                var keyValue = string.Empty;
                if (!string.IsNullOrEmpty(key))
                    keyValue = string.Format("/{0}", key);
                string str = client.DownloadString(Endpoint + uri + keyValue + tokenKey + token);
                if (string.IsNullOrEmpty(str)) {
                    return null;
                }
                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        private static TResponse PostObject<TRequest, TResponse>(string uri, string token, TRequest data) 
            where TRequest : class 
            where TResponse : class {

            const string tokenKey = "?access_token=";
            using (WebClient client = new WebClient()) {
                var value = JsonConvert.SerializeObject(data);
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
                var ret = client.UploadData(Endpoint + uri + tokenKey + token, stream.ToArray());
                if (ret == null || ret.Length == 0) {
                    return null;
                }
                return JsonConvert.DeserializeObject<TResponse>(Encoding.ASCII.GetString(ret));
            }
        }

        private static string GetAttachmentText(string fileType, string fileName, Stream stream) {
            var template = "data:{0};name:{1};base64:{2}";
            byte[] data = new byte[(int)stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return string.Format(template, fileType, fileName, Convert.ToBase64String(data));
        }

        public static ResponseOutgoingFax SendFax(string token, string fileType, string fileName, Stream stream , 
                                                  string recievers, long did, string title = "") {
            var fax = new RequestOutgoingFax() {
                recievers = recievers,
                did = did,
                title = title,
                attachment = GetAttachmentText(fileType, fileName, stream)
            };
            var ret = PostObject<RequestOutgoingFax, ResponseOutgoingFax>("outgoing-faxes", token, fax);
            return ret;
        }

        public static ResponseOutgoingFax SendFax(string token, HttpPostedFileBase file, 
                                                  string recievers, long did, string title = "") {

            return SendFax(token, file.ContentType, Path.GetFileName(file.FileName), file.InputStream, recievers, did, title);
        }

        public static byte[] GetStream(string uri, string token, string key = "") {
            const string tokenKey = "?access_token=";
            using (WebClient client = new WebClient()) {
                var keyValue = string.Empty;
                if (!string.IsNullOrEmpty(key))
                    keyValue = string.Format("/{0}", key);
                return client.DownloadData(Endpoint + uri + keyValue + tokenKey + token);
            }
        }

        /// <summary>
        /// Anonslar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Anons listesi</returns>
        public static List<Announcement> GetAnnouncements(string token) {
            return GetObject<AnnouncementsResponse>("announcement", token).announcements;
        }

        /// <summary>
        /// Otomatik aramalar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Otomatik aramalar</returns>
        public static AutomaticCall GetAutomaticCall(string token) {
            return GetObject<AutomaticCallResponse>("automatic-calls", token).automatic_call;
        }

        /// <summary>
        /// Cdr'lar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Cdr listesi</returns>
        public static List<Cdr> GetCdrs(string token) {
            return GetObject<CdrsResponse>("cdrs", token).cdrs;
        }

        /// <summary>
        /// Cdr
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Cdr id</param>
        /// <returns>Cdr</returns>
        public static Cdr GetCdr(string token, string id) {
            return GetObject<CdrResponse>("cdrs", token, id).cdr;
        }

        /// <summary>
        /// Did'ler
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Did listesi</returns>
        public static List<Did> GetDids(string token) {
            return GetObject<DidsResponse>("dids", token).dids;
        }

        /// <summary>
        /// Did
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Did</returns>
        public static Did GetDid(string token, string id) {
            return GetObject<DidResponse>("dids", token, id).did;
        }

        /// <summary>
        /// Extensions
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns></returns>
        public static List<Extension> GetExtensions(string token) {
            return GetObject<ExtensionsResponse>("extensions", token).extensions;
        }

        /// <summary>
        /// Extension
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Extension</returns>
        public static Extension GetExtension(string token, string id) {
            return GetObject<ExtensionResponse>("extensions", token, id).extension;
        }

        /// <summary>
        /// Gruplar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Grup listesi</returns>
        public static List<Group> GetGroups(string token) {
            return GetObject<GroupsResponse>("groups", token).groups;
        }

        /// <summary>
        /// Grup
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Grup</returns>
        public static Group GetGroup(string token, string id) {
            return GetObject<GroupResponse>("groups", token, id).group;
        }

        /// <summary>
        /// Gelen fakslar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Gelen faks listesi</returns>
        public static List<IncomingFax> GetIncomingFaxes(string token) {
            return GetObject<IncomingFaxesResponse>("incoming-faxes", token).incoming_faxes;
        }

        /// <summary>
        /// Gelen faks STREAM olarak
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Stream olarak faks (TIFF)</returns>
        public static Stream GetIncomingFaxStream(string token, string id) {
            var data = GetStream("incoming-faxes", token, id);
            return new MemoryStream(data);
        }

        /// <summary>
        /// Gelen faks TIFF olarak
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Tiff nesnesi olarak gelen faks</returns>
        public static Tiff GetIncomingFaxAsTiff(string token, string id) {
            return Tiff.ClientOpen("in-memory", "r", GetIncomingFaxStream(token, id), new TiffStream());
        }

        /// <summary>
        /// Gelen faksı dosya olarak indir (TIFF)
        /// Bu metot MVC4 projelerine yöneliktir
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>FileStreamResult olarak faks</returns>
        public static FileStreamResult DownloadIncomingFaxAsTiff(string token, string id) {
            return new FileStreamResult(GetIncomingFaxStream(token, id), "image/tiff");
        }

        /// <summary>
        /// Kullanıcı bilgileri
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Kullanıcı bilgileri</returns>
        public static User GetUser(string token) {
            return GetObject<MeResponse>("me", token).user;
        }

        /// <summary>
        /// PBX
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>PBX</returns>
        public static Pbx GetPbx(string token) {
            return GetObject<MeResponse>("me", token).pbx;
        }

        /// <summary>
        /// Mesajlar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Mesaj listesi</returns>
        public static List<Message> GetMessages(string token) {
            return GetObject<MessagesResponse>("messages", token).messages;
        }

        /// <summary>
        /// Mesaj
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Mesaj</returns>
        public static Message GetMessage(string token, string id) {
            return GetObject<MessageResponse>("messages", token, id).message;
        }

        /// <summary>
        /// Gönderilen fakslar
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Faks listesi</returns>
        public static List<Fax> GetFaxes(string token) {
            return GetObject<OutgoingFaxesResponse>("outgoing-faxes", token).faxes;
        }

        /// <summary>
        /// Gönderilmiş faks
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="id">Id</param>
        /// <returns>Faks</returns>
        public static Fax GetFax(string token, string id) {
            return GetObject<OutgoingFaxResponse>("outgoing-faxes", token, id).fax;
        }
    }
}