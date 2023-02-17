using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using Opapps.Lib.WordpressCracker.Factories;
using Opapps.Lib.WordpressCracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Services.Abstracts
{
    public abstract class WordpressCrackerService
    {
        private readonly HttpClient _httpClient;

        public WordpressCrackerService()
        {
            _httpClient = CreateHttpClient();
        }

        /// <summary>
        /// Create an object of this class with a custom <c>RequestConfiguration</c> object. This is particularly useful to set a proxy.
        /// </summary>
        public WordpressCrackerService(IRequestConfiguration config)
        {
            _httpClient = CreateHttpClientWithConfig(config);
        }
        protected virtual async Task<HttpResponseMessage> PostAsync(Uri loginUrl, FormData loginCredential)
        {
            var formContent = RequestParametersEncoder.GetFormUrlEncodedContentFrom(loginCredential.Username, loginCredential.Password);
            return await _httpClient.PostAsync(loginUrl, formContent);
        }

        private HttpClient CreateHttpClient()
        {
            return HttpClientFactory.Create();
        }

        private HttpClient CreateHttpClientWithConfig(IRequestConfiguration config)
        {
            return HttpClientFactory.Create(config);
        }

        ~WordpressCrackerService()
        {
            _httpClient.Dispose();
        }
    }
}
