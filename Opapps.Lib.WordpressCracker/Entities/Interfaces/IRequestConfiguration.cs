using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Entities.Interfaces
{
    public interface IRequestConfiguration
    {
        public IWebProxy? Proxy { get; set; }
        public bool UseCookies { get; set; }
        public string? UserAgent { get; set; }
        public TimeSpan PooledConnectionLifetime { get; set; }
    }
}
