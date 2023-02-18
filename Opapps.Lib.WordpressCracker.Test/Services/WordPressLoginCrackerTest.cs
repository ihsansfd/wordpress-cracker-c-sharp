using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Services;
using Opapps.Lib.WordpressCracker.Test.Mocks;
using static Opapps.Lib.WordpressCracker.Test.Mocks.WordpressLoginCrackerMockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Services
{
    public class WordPressLoginCrackerTest
    {
        private static readonly Uri TESTING_URL = new Uri("https://some-url.com/wp-login.php");

        [Fact]
        public async Task AttemptLogin_WhenUsernamePasswordCorrect_LoginSuccess()
        {
            var formData = new FormData(CORRECT_USERNAME, CORRECT_PASSWORD);
            var loginService = new WordpressLoginCrackerMock();

            bool result = await loginService.AttemptLoginAsync(TESTING_URL, formData);

            Assert.True(result);
        }

        [Fact]
        public async Task AttemptLogin_WhenUsernamePasswordIncorrect_LoginFailed()
        {
            var formData = new FormData(CORRECT_USERNAME, "wrong");

            var loginService = new WordpressLoginCrackerMock();

            bool result = await loginService.AttemptLoginAsync(TESTING_URL, formData);

            Assert.False(result);
        }
    }
}
