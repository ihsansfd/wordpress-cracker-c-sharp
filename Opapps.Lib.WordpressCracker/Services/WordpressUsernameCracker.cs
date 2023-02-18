using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using Opapps.Lib.WordpressCracker.Helpers;
using Opapps.Lib.WordpressCracker.Helpers.Interfaces;
using Opapps.Lib.WordpressCracker.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Services
{
    public class WordpressUsernameCracker : WordpressCrackerService
    {
        private readonly static IHtmlParser _htmlParser = new HtmlParser();
        public CultureInfo SiteLanguage { get; set; } = new CultureInfo("en");
        public string DummyPassword { get; set; } = "aaaaaaaaa";
        public WordpressUsernameCracker() : base() { }
        public WordpressUsernameCracker(IRequestConfiguration config) : base(config) { }

        //public async Task<bool> AttemptGettingValidUsername(Uri loginUrl, string username)
        //{
        //    using HttpResponseMessage response = await PostAsync(loginUrl, new FormData(username, DummyPassword));
        //    string htmlContent = await response.Content.ReadAsStringAsync();

        //    string? errorMessage = _htmlParser.GetInnerTextWithXpath("//div[@id = 'login_error']", htmlContent);

        //    if (errorMessage != null) return true;

        //    //errorMessage.

        //}

        //public async Task<string> AttemptGettingValidUsernameRange()
        //{

        //}
        //public async Task<List<string>> AttemptGettingValidUsernamesRange()
        //{

        //}

    }
}
