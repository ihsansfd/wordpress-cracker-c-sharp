using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using Opapps.Lib.WordpressCracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Mocks
{
    internal class WordpressLoginCrackerMock : WordpressLoginCracker
    {
        private static readonly string CORRECT_USERNAME = "user";
        private static readonly string CORRECT_PASSWORD = "correct password";
        protected override Task<HttpResponseMessage> PostAsync(Uri loginUrl, FormData loginCredential)
        {
            return Task.FromResult(loginCredential.Username == CORRECT_USERNAME && loginCredential.Password == CORRECT_PASSWORD ?
                GetSuccessfulHttpResponseMessage() : GetFailedHttpResponseMessage());
        }

        private HttpResponseMessage GetFailedHttpResponseMessage()
        {
            return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK };
        }

        private HttpResponseMessage GetSuccessfulHttpResponseMessage()
        {
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                RequestMessage = new HttpRequestMessage() { RequestUri = new Uri("https://some-url.com/wp.login.php") },
            };
        }
    }
}
