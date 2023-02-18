using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Services.Exceptions
{
    public class SiteLangNotAvailableException : Exception
    {
        public SiteLangNotAvailableException() : base("The localized text is not available for the selected site language")
        {
        }
        public SiteLangNotAvailableException(string? message) : base(message)
        {
        }
    }
}
