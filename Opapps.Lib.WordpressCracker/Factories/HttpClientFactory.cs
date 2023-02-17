using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Factories
{
    public static class HttpClientFactory
    {
        private static readonly TimeSpan POOLED_CONNECTION_LIFETIME = TimeSpan.FromMinutes(60);
        public static HttpClient Create()
        {
            var httpHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = POOLED_CONNECTION_LIFETIME
            };
            return new HttpClient(httpHandler);
        }

        public static HttpClient Create(IRequestConfiguration config)
        {
            var httpHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = config.PooledConnectionLifetime,
                UseProxy = config.Proxy != null ? true: false,
                Proxy = config.Proxy,
                UseCookies = config.UseCookies,
            };
            return new HttpClient(httpHandler);
        }
    }
}
