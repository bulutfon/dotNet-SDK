using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override UserInfo ParseUserInfo(string content)
        {
            throw new NotImplementedException();
        }

        protected override Endpoint UserInfoServiceEndpoint
        {
            get { throw new NotImplementedException(); }
        }
    }
}
