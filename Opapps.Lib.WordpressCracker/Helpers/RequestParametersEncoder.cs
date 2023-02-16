using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Helpers
{
    public static class RequestParametersEncoder
    {
        public static FormUrlEncodedContent GetFormUrlEncodedContentFrom(string username, string password)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("log", username),
                new KeyValuePair<string, string>("pwd", password)
            });
        }
    }
}
