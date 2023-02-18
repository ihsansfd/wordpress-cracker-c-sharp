using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Mocks
{
    public static class WordPressUsernameCrackerMockData {
        public static readonly string VALID_USERNAME = "user";
    }
    internal class WordpressUsernameCrackerMock : WordpressUsernameCracker
    {
        private static readonly string VALID_USERNAME = WordPressUsernameCrackerMockData.VALID_USERNAME;
        public string TheirUsername { get; set; } = VALID_USERNAME;
        protected override Task<HttpResponseMessage> PostAsync(Uri loginUrl, FormData loginCredential)
        {
            TheirUsername = loginCredential.Username;
            return Task.FromResult(new HttpResponseMessage());
        }

        protected override Task<string> GetHtmlContent(HttpResponseMessage response)
        {
            string res = string.Format("<div id='login_error'>{0}</div>", 
                VALID_USERNAME == TheirUsername ? GetUsernameValidMessage() : Config.UsernameErrorMessage);

            return Task.FromResult(res);
        }

        private static string GetUsernameValidMessage()
        {
            return "Wrong Password";
        }

    }
}
