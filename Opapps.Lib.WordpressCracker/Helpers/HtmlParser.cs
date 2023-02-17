using HtmlAgilityPack;
using Opapps.Lib.WordpressCracker.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Helpers
{
    public class HtmlParser : IHtmlParser
    {
        public HtmlDocument ParseHtml(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }

        public HtmlNode SelectNodeWithXpath(string xpathPattern, string html)
        {
            var htmlDoc = ParseHtml(html);
            return htmlDoc.DocumentNode.SelectSingleNode(xpathPattern);

        }

        public string GetInnerTextWithXpath(string xpathPattern, string html)
        {
            return SelectNodeWithXpath(xpathPattern, html).InnerText;
        }
    }
}
