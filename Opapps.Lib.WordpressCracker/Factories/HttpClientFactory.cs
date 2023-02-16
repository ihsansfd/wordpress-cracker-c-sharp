using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Factories
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateDefaultHttpClient()
        {
            var httpHandler = new SocketsHttpHandler
            {
                AllowAutoRedirect = false,
                PooledConnectionLifetime = TimeSpan.FromMinutes(60)
            };
            return new HttpClient(httpHandler);
        }
    }
}
