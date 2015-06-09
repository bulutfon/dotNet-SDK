using Bulutfon.Models;
using Newtonsoft.Json.Linq;
using OAuth2.Client;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;

namespace Bulutfon.OAuth2.Client
{
    /// <summary>
    /// Bulutfon authentication client
    /// </summary>
    public class BulutfonClient : OAuth2Client
    {
        /// <summary>
        /// <see cref="BulutfonClient"/> nesnesi oluşturur
        /// </summary>
        /// <param name="factory">Request factory</param>
        /// <param name="configuration">Konfigürasyon</param>
        protected BulutfonClient(IRequestFactory factory, IClientConfiguration configuration)
            : base(factory, configuration)
        {
        }

        /// <summary>
        /// Koda erişen servis için URI sağlar
        /// </summary>
        protected override Endpoint AccessCodeServiceEndpoint
        {
            get 
            { 
                return new Endpoint 
                {
                    BaseUri = "https://www.bulutfon.com",
                    Resource = "/oauth/authorize"
                };
            }
        }

        /// <summary>
        /// Token'a erişen servis için URI sağlar
        /// </summary>
        protected override Endpoint AccessTokenServiceEndpoint
        {
            get 
            { 
                return new Endpoint 
                {
                    BaseUri = "https://www.bulutfon.com",
                    Resource = "/oauth/token"
                };
            }
        }

        public override string Name
        {
            get { return "Bulutfon API"; }
        }

        /// <summary>
        /// Servisten alınan içeriğe göre <see cref="UserInfo"/> oluşturur ve döndürür
        /// </summary>
        /// <param name="content">Servisin sağladığı içerik</param>
        /// <returns>Kullanıcı bilgileri</returns>
        protected override UserInfo ParseUserInfo(string content)
        {
            var response = new JObject(content);
            return new BulutfonUser()
            {
                Id = response["user"]["email"].Value<string>(),
                Name = response["user"]["name"].Value<string>(),
                Gsm = response["gsm"].Value<string>(),
                Email = response["user"]["email"].Value<string>(),

                Pbx = new Pbx() 
                {
                    Name = response["pbx"]["name"].Value<string>(),
                    Url = response["pbx"]["url"].Value<string>(),
                    State = response["pbx"]["state"].Value<string>(),
                    Package = response["pbx"]["package"].Value<string>(),
                    CustomerType = response["pbx"]["customer_type"].Value<string>()
                },
                Credit = response["credit"].Value<double>()
            };
        }

        /// <summary>
        /// Giriş yapmış olan kullanıcının bilgilerine erişim sağlayan URI
        /// </summary>
        protected override Endpoint UserInfoServiceEndpoint
        {
            get 
            { 
                return new Endpoint 
                {
                    BaseUri = "https://www.bulutfon.com",
                    Resource = string.Format("/me?{0}", AccessToken)
                };
            }
        }
    }
}