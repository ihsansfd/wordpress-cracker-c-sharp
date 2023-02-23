using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Opapps.Lib.WordpressCracker.Test.Mocks.WordPressUsernameCrackerMockData;

namespace Opapps.Lib.WordpressCracker.Test.Mocks
{
    public static class WordPressUsernameCrackerMockData {
        public static readonly string VALID_USERNAME = "user";
        public static readonly string IP_GET_BLOCKED_USERNAME = "blockedUsername";
        public static readonly string INVALID_USERNAME = "invalid";
    }
    internal class WordpressUsernameCrackerMock : WordpressUsernameCracker
    {
        public string TheirUsername { get; set; } = String.Empty;
        protected override Task<HttpResponseMessage> PostAsync(Uri loginUrl, FormData loginCredential)
        {
            TheirUsername = loginCredential.Username;
            return Task.FromResult(new HttpResponseMessage());
        }

        protected override Task<string> GetHtmlContent(HttpResponseMessage response)
        {

            string errorMessageHtml = DetermineErrorMessageHtml(TheirUsername);

            return Task.FromResult(errorMessageHtml);
        }

        private string DetermineErrorMessageHtml(string theirUsername)
        {
            if (theirUsername == VALID_USERNAME) return GetInvalidPasswordMessageHtml();
            if (theirUsername == IP_GET_BLOCKED_USERNAME) return GetIpGetBlockedErrorMessageHtml();

            return GetInvalidUsernameErrorMessageHtml();
        }

        private static string GetInvalidPasswordMessageHtml()
        {
            return "<div id=\"login_error\">" +
                "<strong>Eror</strong>: Kata sandi yang Anda masukkan untuk pengguna " +
                "<strong>user</strong> tidak cocok. " +
                "<a href=\"http://some-url.com/wp-login.php?action=lostpassword\">Lupa sandi Anda?</a><br></div>";
        }

        private static string GetIpGetBlockedErrorMessageHtml()
        {
            return "<div id=\"login_error\">" +
                "<strong>Eror</strong>: Sorry your ip address get blocked. 0 Attempt left. " +
                "</div>";
        }

        private static string GetInvalidUsernameErrorMessageHtml()
        {
            return "<div id=\"login_error\">" +
                "<strong>Error:</strong> The username <strong>ida</strong> is not registered on this site. " +
                "If you are unsure of your username, try your email address instead.<br></div>";
        }
    }
}
