using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Entities
{
    public class RequestConfiguration : IRequestConfiguration
{
        public IWebProxy? Proxy { get; set; }
        public bool UseCookies { get; set; }
        public TimeSpan PooledConnectionLifetime { get; set; } = TimeSpan.FromMinutes(60);
    }
}
