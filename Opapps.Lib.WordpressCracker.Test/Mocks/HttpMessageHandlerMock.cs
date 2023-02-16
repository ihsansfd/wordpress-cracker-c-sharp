using Opapps.Lib.WordpressCracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Mocks
{

    internal static class HttpMessageHandlerMockFactory {
        private static readonly string CORRECT_USERNAME = "user";
        private static readonly string CORRECT_PASSWORD = "correct password";

        public static HttpMessageHandler Create(FormData formData)
        {
            return formData.Username == CORRECT_USERNAME && formData.Password == CORRECT_PASSWORD ?
                new HttpMessageHandlerSuccessMock() : new HttpMessageHandlerFailedMock();
        }
    }
    internal class HttpMessageHandlerFailedMock : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(){ StatusCode = System.Net.HttpStatusCode.OK });
        }
    }

    internal class HttpMessageHandlerSuccessMock : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Redirect,
                Headers = { Location = new Uri("https://some-url.com/wp-admin") }
            });
        }
    }
}
