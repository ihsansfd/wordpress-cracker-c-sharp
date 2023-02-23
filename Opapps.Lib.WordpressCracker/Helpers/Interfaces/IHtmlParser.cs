using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Helpers.Interfaces
{
    public interface IHtmlParser
    { 
        public string? GetInnerTextWithXpath(string xpathPattern, string html);
        string? GetOuterHtmlWithXpath(string v, string htmlContent);
    }
}
