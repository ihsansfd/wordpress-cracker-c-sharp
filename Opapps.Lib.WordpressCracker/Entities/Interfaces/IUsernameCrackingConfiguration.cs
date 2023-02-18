using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Entities.Interfaces
{
    public interface IUsernameCrackingConfiguration
    {
        public bool AutoDetectUsernameErrorMessage { get; set; }
        public string UsernameErrorMessage { get; set; }

        public string DummyPassword { get; set; }

    }
}
