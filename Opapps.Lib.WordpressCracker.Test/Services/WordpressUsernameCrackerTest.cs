using Opapps.Lib.WordpressCracker.Services;
using Opapps.Lib.WordpressCracker.Test.Mocks;
using static Opapps.Lib.WordpressCracker.Test.Mocks.WordPressUsernameCrackerMockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Services
{

    // TODO : Implement the mock (currently is using real testing site)
    public class WordpressUsernameCrackerTest
    {
        private readonly WordpressUsernameCracker _service;
        private readonly static Uri TESTING_URL = new Uri("http://some-url.com/wp-login.php");
        public WordpressUsernameCrackerTest()
        {
            _service = new WordpressUsernameCrackerMock();
        }

        [Fact]
        public async Task AttemptGettingValidUsername_WhenUsernameIsInvalid_ReturnFalse()
        {
            bool res = await _service.AttemptGettingValidUsername(TESTING_URL, INVALID_USERNAME);
            Assert.False(res);
        } 
        
        [Fact]
        public async Task AttemptGettingValidUsername_WhenUsernameIsValid_ReturnTrue()
        {
            bool res = await _service.AttemptGettingValidUsername(TESTING_URL, VALID_USERNAME);
            Assert.True(res);
        }

        [Fact]
        public async Task AttemptGettingValidUsername_WhenErrorMessageOtherThanInvalidPassword_UsernameIsInvalid_ReturnFalse()
        {
            bool res = await _service.AttemptGettingValidUsername(TESTING_URL, IP_GET_BLOCKED_USERNAME);
            Assert.False(res);
        }
    }
}
