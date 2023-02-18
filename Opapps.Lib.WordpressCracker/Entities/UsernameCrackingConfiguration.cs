using Opapps.Lib.WordpressCracker.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Entities
{
    public class UsernameCrackingConfiguration : IUsernameCrackingConfiguration
    {
        public bool AutoDetectUsernameErrorMessage { get; set; } = false;
        public string UsernameErrorMessage { get; set; } = "Error: The username is not registered on this site. If you are unsure of your username, try your email address instead.";
        public string DummyPassword { get; set; } = "aaaaaaaaa";
    }
}
