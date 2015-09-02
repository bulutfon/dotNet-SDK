using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using BitMiracle.LibTiff.Classic;
using Bulutfon.Sdk.Models;
using Bulutfon.Sdk.Models.ResponseObjects;
using Bulutfon.OAuth;
using Newtonsoft.Json;
using Bulutfon.Sdk.Models.Post;

namespace Bulutfon.Sdk {

    /// <summary>
    /// Bulutfon Servislerine Ulaşmak İçin Gerekli Metotları Sağlar
    /// </summary>
    public static class BulutfonApi {

        /// <summary>
        /// https://api.bulutfon.com/
        /// </summary>
        public const string BaseUri = "https://api.bulutfon.com/";

        /// <summary>
        /// JSON serialize/deserialize ayarları
        /// </summary>
        /// <returns>JsonSerializerSettings</returns>
        private static JsonSerializerSettings SerializerSettings() { 
            return new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
        }

        /// <summary>
        /// GET (REST)
        /// </summary>
        /// <typeparam name="T">Nesne sınıfı</typeparam>
        /// <param name="uri">Adres</param>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="key">Id (opsiyonel)</param>
        /// <returns>Servisten dönen nesne</returns>
        public static T GetObject<T>(string uri, Token token, string key = "") where T : class {
            const string tokenKey = "?access_token=";
            try {
                using (WebClient client = new WebClient()) {
                    var keyValue = string.Empty;
                    if (!string.IsNullOrEmpty(key))
                        keyValue = string.Format("/{0}", key);
                    client.Encoding = ASCIIEncoding.UTF8;
                    string str = client.DownloadString(BaseUri + uri + keyValue + tokenKey + token.AccessToken);
                    if (string.IsNullOrEmpty(str)) {
                        return null;
                    }
                    return JsonConvert.DeserializeObject<T>(str, SerializerSettings());
                }
            }
            catch (Exception e) {
                if (e.Message.ToLower().Contains("expired")) {
                    token.RefreshAccessToken();
                    return GetObject<T>(uri, token, key);
                }
                else {
                    throw;
                }
            }
        }

        /// <summary>
        /// POST (REST)
        /// </summary>
        /// <typeparam name="TPostObject">Request sınıfı</typeparam>
        /// <typeparam name="TResponse">Response sınıfı</typeparam>
        /// <param name="uri">Adres</param>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="data">Veri (nesne)</param>
        /// <returns>Servisten dönen nesne</returns>
        public static TResponse PostObject<TPostObject, TResponse>(string uri, Token token, TPostObject data) 
            where TPostObject : class 
            where TResponse : class {

            const string tokenKey = "?access_token=";
            try {
                using (WebClient client = new WebClient()) {
                    var value = JsonConvert.SerializeObject(data, Formatting.None, SerializerSettings());
                    var stream = new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.ContentEncoding] = "UTF-8";
                    var ret = client.UploadData(BaseUri + uri + tokenKey + token.AccessToken, stream.ToArray());
                    if (ret == null || ret.Length == 0) {
                        return null;
                    }
                    return JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(ret), SerializerSettings());
                }
            }
            catch (Exception e) {
                if (e.Message.ToLower().Contains("expired")) {
                    token.RefreshAccessToken();
                    return PostObject<TPostObject, TResponse>(uri, token, data);
                }
                else {
                    throw;
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
        /// <param name="token">Access token</param>
        /// <param name="fileType">Dosya türü</param>
        /// <param name="fileName">Dosya adı</param>
        /// <param name="stream">Binary formatta dosya</param>
        /// <param name="receivers">Alıcılar</param>
        /// <param name="did">Gönderen</param>
        /// <param name="title">Başlık</param>
        /// <returns>Gönderim durumu</returns>
        public static Bulutfon.Sdk.Models.Post.ResponseOutgoingFax SendFax(Token token, string fileType, string fileName, Stream stream , 
                                                  string receivers, long did, string title = "") {
            var fax = new Bulutfon.Sdk.Models.Post.RequestOutgoingFax() {
                receivers = receivers,
                did = did,
                title = title,
                attachment = GetAttachmentText(fileType, fileName, stream)
            };
            return PostObject<Bulutfon.Sdk.Models.Post.RequestOutgoingFax, Bulutfon.Sdk.Models.Post.ResponseOutgoingFax>("outgoing-faxes", token, fax);
        }

        /// <summary>
        /// SMS Gönder
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="message">Mesaj nesnesi</param>
        /// <returns>Gönderim durumu</returns>
        public static Bulutfon.Sdk.Models.Post.ResponseSendMessage SendSms(Token token, Bulutfon.Sdk.Models.Post.RequestSendMessage message) {
            return PostObject<Bulutfon.Sdk.Models.Post.RequestSendMessage, Bulutfon.Sdk.Models.Post.ResponseSendMessage>("messages", token, message);
        }

        /// <summary>
        /// SMS Gönder
        /// </summary>
        /// <param name="token">Access token</param>
        /// <param name="msgTitle">Başlık</param>
        /// <param name="msgReceivers">Alıcılar</param>
        /// <param name="msgContent">İçerik (mesaj metni)</param>
        /// <param name="isSingleSms">Tek SMS'te birleştirilsin mi?</param>
        /// <param name="isFutureSms">Sonra gönderilsin</param>
        /// <param name="sendDate">Gönderim tarihi (sonra gönderilecekse)</param>
        /// <returns>Gönderim durumu</returns>
        public static Bulutfon.Sdk.Models.Post.ResponseSendMessage SendSms(Token token,
            string msgTitle, string msgReceivers, string msgContent, bool isSingleSms, bool isFutureSms, DateTime sendDate) {

            var message = new Bulutfon.Sdk.Models.Post.RequestSendMessage() {
                title = msgTitle,
                receivers = msgReceivers,
                content = msgContent,
                is_single_sms = isSingleSms,
                is_future_sms = isFutureSms//,
                //send_date = sendDate
            };
            return SendSms(token, message);
        }

        /// <summary>
        /// Faks gönder
        /// </summary>
        /// <param name="token">Acess token</param>
        /// <param name="file">Dosya</param>
        /// <param name="receivers">Alıcılar</param>
        /// <param name="did">Gönderen</param>
        /// <param name="title">Başlık</param>
        /// <returns>Gönderim durumu</returns>
        public static Bulutfon.Sdk.Models.Post.ResponseOutgoingFax SendFax(Token token, HttpPostedFileBase file, 
                                                  string receivers, long did, string title = "") {

            return SendFax(token, file.ContentType, Path.GetFileName(file.FileName), file.InputStream, receivers, did, title);
        }

        /// <summary>
        /// Dosya indir
        /// </summary>
        /// <param name="uri">Adres</param>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="key">Id</param>
        /// <returns>Binary formatta dosya</returns>
        public static byte[] GetStream(string uri, Token token, string key = "") {
            const string tokenKey = "?access_token=";
            try {
                using (WebClient client = new WebClient()) {
                    var keyValue = string.Empty;
                    if (!string.IsNullOrEmpty(key))
                        keyValue = string.Format("/{0}", key);
                    return client.DownloadData(BaseUri + uri + keyValue + tokenKey + token.AccessToken);
                }
            }
            catch (Exception e) {
                if (e.Message.ToLower().Contains("expired")) {
                    token.RefreshAccessToken();
                    return GetStream(uri, token, key);
                }
                else {
                    throw;
                }
            }
        }

        /// <summary>
        /// Anonslar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Anons listesi</returns>
        public static List<Bulutfon.Sdk.Models.Announcement> GetAnnouncements(Token token) {
            return GetObject<Bulutfon.Sdk.Models.ResponseObjects.AnnouncementsResponse>("announcements", token).announcements;
        }

        /// <summary>
        /// Otomatik aramalar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Otomatik aramalar</returns>
        public static AutomaticCall GetAutomaticCall(Token token) {
            return GetObject<AutomaticCallResponse>("automatic-calls", token).automatic_call;
        }

        public static ResponseAutomaticCall CreateAutomaticCall(Token token, AutomaticCallCreator automaticCall) {
            return PostObject<AutomaticCallCreator, ResponseAutomaticCall>("automatic-calls", token, automaticCall);
        }

        /// <summary>
        /// Cdr'lar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Cdr listesi</returns>
        public static List<Cdr> GetCdrs(Token token) {
            return GetObject<CdrsResponse>("cdrs", token).cdrs;
        }

        /// <summary>
        /// Cdr
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Cdr id</param>
        /// <returns>Cdr</returns>
        public static Cdr GetCdr(Token token, string id) {
            return GetObject<CdrResponse>("cdrs", token, id).cdr;
        }

        /// <summary>
        /// Did'ler
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Did listesi</returns>
        public static List<Did> GetDids(Token token) {
            return GetObject<DidsResponse>("dids", token).dids;
        }

        /// <summary>
        /// Did
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Did</returns>
        public static Did GetDid(Token token, string id) {
            return GetObject<DidResponse>("dids", token, id).did;
        }

        /// <summary>
        /// Extensions
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns></returns>
        public static List<Extension> GetExtensions(Token token) {
            return GetObject<ExtensionsResponse>("extensions", token).extensions;
        }

        /// <summary>
        /// Extension
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Extension</returns>
        public static Extension GetExtension(Token token, string id) {
            return GetObject<ExtensionResponse>("extensions", token, id).extension;
        }

        /// <summary>
        /// Gruplar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Grup listesi</returns>
        public static List<Group> GetGroups(Token token) {
            return GetObject<GroupsResponse>("groups", token).groups;
        }

        /// <summary>
        /// Grup
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Grup</returns>
        public static Group GetGroup(Token token, string id) {
            return GetObject<GroupResponse>("groups", token, id).group;
        }

        /// <summary>
        /// Gelen fakslar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Gelen faks listesi</returns>
        public static List<IncomingFax> GetIncomingFaxes(Token token) {
            return GetObject<IncomingFaxesResponse>("incoming-faxes", token).incoming_faxes;
        }

        /// <summary>
        /// Gelen faks STREAM olarak
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Stream olarak faks (TIFF)</returns>
        public static Stream GetIncomingFaxStream(Token token, string id) {
            var data = GetStream("incoming-faxes", token, id);
            return new MemoryStream(data);
        }

        /// <summary>
        /// Gelen faks TIFF olarak
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Tiff nesnesi olarak gelen faks</returns>
        public static Tiff GetIncomingFaxAsTiff(Token token, string id) {
            return Tiff.ClientOpen("in-memory", "r", GetIncomingFaxStream(token, id), new TiffStream());
        }

        ///// <summary>
        ///// Gelen faksı dosya olarak indir (TIFF)
        ///// Bu metot MVC4 projelerine yöneliktir
        ///// </summary>
        ///// <param name="token">Token provider (access ve refresh token)</param>
        ///// <param name="id">Id</param>
        ///// <returns>FileStreamResult olarak faks</returns>
        //public static FileStreamResult DownloadIncomingFaxAsTiff(TokenProvider token, string id) {
        //    return new FileStreamResult(GetIncomingFaxStream(token, id), "image/tiff");
        //}

        /// <summary>
        /// Kullanıcı bilgileri
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Kullanıcı bilgileri</returns>
        public static User GetUser(Token token) {
            return GetObject<MeResponse>("me", token).user;
        }

        /// <summary>
        /// PBX
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>PBX</returns>
        public static Pbx GetPbx(Token token) {
            return GetObject<MeResponse>("me", token).pbx;
        }

        /// <summary>
        /// Mesajlar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Mesaj listesi</returns>
        public static List<Bulutfon.Sdk.Models.Message> GetMessages(Token token) {
            return GetObject<MessagesResponse>("messages", token).messages;
        }

        /// <summary>
        /// Mesaj
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Mesaj</returns>
        public static Bulutfon.Sdk.Models.Message GetMessage(Token token, string id) {
            return GetObject<MessageResponse>("messages", token, id).message;
        }

        /// <summary>
        /// Gönderilen fakslar
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <returns>Faks listesi</returns>
        public static List<Fax> GetFaxes(Token token) {
            return GetObject<OutgoingFaxesResponse>("outgoing-faxes", token).faxes;
        }

        /// <summary>
        /// Gönderilmiş faks
        /// </summary>
        /// <param name="token">Token provider (access ve refresh token)</param>
        /// <param name="id">Id</param>
        /// <returns>Faks</returns>
        public static Fax GetFax(Token token, string id) {
            return GetObject<OutgoingFaxResponse>("outgoing-faxes", token, id).fax;
        }
    }
}