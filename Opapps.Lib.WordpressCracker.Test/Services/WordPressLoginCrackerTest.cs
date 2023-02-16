using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Services;
using Opapps.Lib.WordpressCracker.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Services
{
    public class WordPressLoginCrackerTest
    {
        private static readonly string TESTING_URL = "https://some-url.com/wp-login.php";

        [Fact]
        public async Task AttemptLogin_WhenUsernamePasswordCorrect_LoginSuccess()
        {
            var formData = new FormData("user", "correct password");
            var loginService = new WordpressLoginCracker(new HttpClient(HttpMessageHandlerMockFactory.Create(formData)));

            bool result = await loginService.AttemptLoginAsync(TESTING_URL, formData);

            Assert.True(result);
        }

        [Fact]
        public async Task AttemptLogin_WhenUsernamePasswordIncorrect_LoginFailed()
        {
            var formData = new FormData("user", "wrong password");

            var loginService = new WordpressLoginCracker(new HttpClient(HttpMessageHandlerMockFactory.Create(formData)));

            bool result = await loginService.AttemptLoginAsync(TESTING_URL, formData);

            Assert.False(result);
        }
    }
}
