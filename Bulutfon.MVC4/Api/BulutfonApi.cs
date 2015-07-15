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

    /// <summary>
    /// Bulutfon Servislerine Ulaşmak İçin Gerekli Metotları Sağlar
    /// </summary>
    public static class BulutfonApi {

        public const string TokenProviderKey = "token_provider";

        /// <summary>
        /// https://api.bulutfon.com/
        /// </summary>
        public const string BaseUri = "https://api.bulutfon.com/";

        /// <summary>
        /// GET (REST)
        /// </summary>
        /// <typeparam name="T">Nesne sınıfı</typeparam>
        /// <param name="uri">Adres</param>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="key">Id (opsiyonel)</param>
        /// <returns>Servisten dönen nesne</returns>
        public static T GetObject<T>(string uri, TokenProvider tokenProvider, string key = "") where T : class {
            const string tokenKey = "?access_token=";
            try {
                using (WebClient client = new WebClient()) {
                    var keyValue = string.Empty;
                    if (!string.IsNullOrEmpty(key))
                        keyValue = string.Format("/{0}", key);
                    string str = client.DownloadString(BaseUri + uri + keyValue + tokenKey + tokenProvider.AccessToken);
                    if (string.IsNullOrEmpty(str)) {
                        return null;
                    }
                    return JsonConvert.DeserializeObject<T>(str);
                }
            }
            catch (Exception e) {
                if (e.Message.ToLower().Contains("expired")) {
                    tokenProvider.RefreshAccessToken();
                    return GetObject<T>(uri, tokenProvider, key);
                }
                else {
                    throw e;
                }
            }
        }

        /// <summary>
        /// POST (REST)
        /// </summary>
        /// <typeparam name="TRequest">Request sınıfı</typeparam>
        /// <typeparam name="TResponse">Response sınıfı</typeparam>
        /// <param name="uri">Adres</param>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="data">Veri (nesne)</param>
        /// <returns>Servisten dönen nesne</returns>
        public static TResponse PostObject<TRequest, TResponse>(string uri, TokenProvider tokenProvider, TRequest data) 
            where TRequest : class 
            where TResponse : class {

            const string tokenKey = "?access_token=";
            try {
                using (WebClient client = new WebClient()) {
                    var settings = new JsonSerializerSettings() {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat //DateFormatHandling.IsoDateFormat?
                    };
                    var value = JsonConvert.SerializeObject(data, Formatting.None, settings);
                    var stream = new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.ContentEncoding] = "UTF-8";
                    var ret = client.UploadData(BaseUri + uri + tokenKey + tokenProvider.AccessToken, stream.ToArray());
                    if (ret == null || ret.Length == 0) {
                        return null;
                    }
                    return JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(ret));
                }
            }
            catch (Exception e) {
                if (e.Message.ToLower().Contains("expired")) {
                    tokenProvider.RefreshAccessToken();
                    return PostObject<TRequest, TResponse>(uri, tokenProvider, data);
                }
                else {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gönderilen faks dosyasını metin olarak (base64) hazırlar
        /// </summary>
        /// <param name="fileType">Dosya türü</param>
        /// <param name="fileName">Dosya adı</param>
        /// <param name="stream">Binary formatta dosya</param>
        /// <returns></returns>
        private static string GetAttachmentText(string fileType, string fileName, Stream stream) {
            var template = "data:{0};name:{1};base64:{2}";
            byte[] data = new byte[(int)stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return string.Format(template, fileType, fileName, Convert.ToBase64String(data));
        }

        /// <summary>
        /// Faks gönder
        /// </summary>
        /// <param name="tokenProvider">Acess token</param>
        /// <param name="fileType">Dosya türü</param>
        /// <param name="fileName">Dosya adı</param>
        /// <param name="stream">Binary formatta dosya</param>
        /// <param name="receivers">Alıcılar</param>
        /// <param name="did">Gönderen</param>
        /// <param name="title">Başlık</param>
        /// <returns>Gönderim durumu</returns>
        public static ResponseOutgoingFax SendFax(TokenProvider tokenProvider, string fileType, string fileName, Stream stream , 
                                                  string receivers, long did, string title = "") {
            var fax = new RequestOutgoingFax() {
                receivers = receivers,
                did = did,
                title = title,
                attachment = GetAttachmentText(fileType, fileName, stream)
            };
            return PostObject<RequestOutgoingFax, ResponseOutgoingFax>("outgoing-faxes", tokenProvider, fax);
        }

        public static ResponseSendMessage SendSms(TokenProvider tokenProvider, RequestSendMessage message) {
            return PostObject<RequestSendMessage, ResponseSendMessage>("messages", tokenProvider, message);
        }

        public static ResponseSendMessage SendSms(TokenProvider tokenProvider,
            string msgTitle, string msgReceivers, string msgContent, bool isSingleSms, bool isFutureSms, DateTime sendDate) {

            var message = new RequestSendMessage() {
                title = msgTitle,
                receivers = msgReceivers,
                content = msgContent,
                is_single_sms = isSingleSms,
                is_future_sms = isFutureSms//,
                //send_date = sendDate
            };
            return SendSms(tokenProvider, message);
        }

        /// <summary>
        /// Faks gönder
        /// </summary>
        /// <param name="tokenProvider">Acess token</param>
        /// <param name="file">Dosya</param>
        /// <param name="receivers">Alıcılar</param>
        /// <param name="did">Gönderen</param>
        /// <param name="title">Başlık</param>
        /// <returns>Gönderim durumu</returns>
        public static ResponseOutgoingFax SendFax(TokenProvider tokenProvider, HttpPostedFileBase file, 
                                                  string receivers, long did, string title = "") {

            return SendFax(tokenProvider, file.ContentType, Path.GetFileName(file.FileName), file.InputStream, receivers, did, title);
        }

        /// <summary>
        /// Dosya indir
        /// </summary>
        /// <param name="uri">Adres</param>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="key">Id</param>
        /// <returns>Binary formatta dosya</returns>
        public static byte[] GetStream(string uri, TokenProvider tokenProvider, string key = "") {
            const string tokenKey = "?access_token=";
            try {
                using (WebClient client = new WebClient()) {
                    var keyValue = string.Empty;
                    if (!string.IsNullOrEmpty(key))
                        keyValue = string.Format("/{0}", key);
                    return client.DownloadData(BaseUri + uri + keyValue + tokenKey + tokenProvider.AccessToken);
                }
            }
            catch (Exception e) {
                if (e.Message.ToLower().Contains("expired")) {
                    tokenProvider.RefreshAccessToken();
                    return GetStream(uri, tokenProvider, key);
                }
                else {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Anonslar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Anons listesi</returns>
        public static List<Announcement> GetAnnouncements(TokenProvider tokenProvider) {
            return GetObject<AnnouncementsResponse>("announcement", tokenProvider).announcements;
        }

        /// <summary>
        /// Otomatik aramalar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Otomatik aramalar</returns>
        public static AutomaticCall GetAutomaticCall(TokenProvider tokenProvider) {
            return GetObject<AutomaticCallResponse>("automatic-calls", tokenProvider).automatic_call;
        }

        /// <summary>
        /// Cdr'lar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Cdr listesi</returns>
        public static List<Cdr> GetCdrs(TokenProvider tokenProvider) {
            return GetObject<CdrsResponse>("cdrs", tokenProvider).cdrs;
        }

        /// <summary>
        /// Cdr
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Cdr id</param>
        /// <returns>Cdr</returns>
        public static Cdr GetCdr(TokenProvider tokenProvider, string id) {
            return GetObject<CdrResponse>("cdrs", tokenProvider, id).cdr;
        }

        /// <summary>
        /// Did'ler
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Did listesi</returns>
        public static List<Did> GetDids(TokenProvider tokenProvider) {
            return GetObject<DidsResponse>("dids", tokenProvider).dids;
        }

        /// <summary>
        /// Did
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Did</returns>
        public static Did GetDid(TokenProvider tokenProvider, string id) {
            return GetObject<DidResponse>("dids", tokenProvider, id).did;
        }

        /// <summary>
        /// Extensions
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns></returns>
        public static List<Extension> GetExtensions(TokenProvider tokenProvider) {
            return GetObject<ExtensionsResponse>("extensions", tokenProvider).extensions;
        }

        /// <summary>
        /// Extension
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Extension</returns>
        public static Extension GetExtension(TokenProvider tokenProvider, string id) {
            return GetObject<ExtensionResponse>("extensions", tokenProvider, id).extension;
        }

        /// <summary>
        /// Gruplar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Grup listesi</returns>
        public static List<Group> GetGroups(TokenProvider tokenProvider) {
            return GetObject<GroupsResponse>("groups", tokenProvider).groups;
        }

        /// <summary>
        /// Grup
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Grup</returns>
        public static Group GetGroup(TokenProvider tokenProvider, string id) {
            return GetObject<GroupResponse>("groups", tokenProvider, id).group;
        }

        /// <summary>
        /// Gelen fakslar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Gelen faks listesi</returns>
        public static List<IncomingFax> GetIncomingFaxes(TokenProvider tokenProvider) {
            return GetObject<IncomingFaxesResponse>("incoming-faxes", tokenProvider).incoming_faxes;
        }

        /// <summary>
        /// Gelen faks STREAM olarak
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Stream olarak faks (TIFF)</returns>
        public static Stream GetIncomingFaxStream(TokenProvider tokenProvider, string id) {
            var data = GetStream("incoming-faxes", tokenProvider, id);
            return new MemoryStream(data);
        }

        /// <summary>
        /// Gelen faks TIFF olarak
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Tiff nesnesi olarak gelen faks</returns>
        public static Tiff GetIncomingFaxAsTiff(TokenProvider tokenProvider, string id) {
            return Tiff.ClientOpen("in-memory", "r", GetIncomingFaxStream(tokenProvider, id), new TiffStream());
        }

        /// <summary>
        /// Gelen faksı dosya olarak indir (TIFF)
        /// Bu metot MVC4 projelerine yöneliktir
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>FileStreamResult olarak faks</returns>
        public static FileStreamResult DownloadIncomingFaxAsTiff(TokenProvider tokenProvider, string id) {
            return new FileStreamResult(GetIncomingFaxStream(tokenProvider, id), "image/tiff");
        }

        /// <summary>
        /// Kullanıcı bilgileri
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Kullanıcı bilgileri</returns>
        public static User GetUser(TokenProvider tokenProvider) {
            return GetObject<MeResponse>("me", tokenProvider).user;
        }

        /// <summary>
        /// PBX
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>PBX</returns>
        public static Pbx GetPbx(TokenProvider tokenProvider) {
            return GetObject<MeResponse>("me", tokenProvider).pbx;
        }

        /// <summary>
        /// Mesajlar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Mesaj listesi</returns>
        public static List<Message> GetMessages(TokenProvider tokenProvider) {
            return GetObject<MessagesResponse>("messages", tokenProvider).messages;
        }

        /// <summary>
        /// Mesaj
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Mesaj</returns>
        public static Message GetMessage(TokenProvider tokenProvider, string id) {
            return GetObject<MessageResponse>("messages", tokenProvider, id).message;
        }

        /// <summary>
        /// Gönderilen fakslar
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <returns>Faks listesi</returns>
        public static List<Fax> GetFaxes(TokenProvider tokenProvider) {
            return GetObject<OutgoingFaxesResponse>("outgoing-faxes", tokenProvider).faxes;
        }

        /// <summary>
        /// Gönderilmiş faks
        /// </summary>
        /// <param name="tokenProvider">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Faks</returns>
        public static Fax GetFax(TokenProvider tokenProvider, string id) {
            return GetObject<OutgoingFaxResponse>("outgoing-faxes", tokenProvider, id).fax;
        }
    }
}