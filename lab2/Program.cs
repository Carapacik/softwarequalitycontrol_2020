using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace LinksChecker
{
    internal static class Program
    {
        private const string Protocol = "http";
        private const string Url = "http://91.210.252.240/broken-links/";

        private static bool IsContainProtocol(string link)
        {
            return link.Contains(Protocol);
        }

        private static bool IsLinkCorrect(string link)
        {
            return !string.IsNullOrEmpty(link) && !link.StartsWith("#");
        }

        private static string FormFullLink(string link)
        {
            return IsContainProtocol(link) ? link : Url + link;
        }

        private static void WriteLinks(string link, StreamWriter invalidLinksFile, StreamWriter validLinksFile)
        {
            var fullLink = FormFullLink(link);
            var request = (HttpWebRequest) WebRequest.Create(fullLink);
            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                var message = $"{fullLink} {response.StatusCode.GetHashCode()} {response.StatusDescription}";
                validLinksFile.WriteLine(message);
            }
            catch (WebException e)
            {
                var message =
                    $"{fullLink} {((HttpWebResponse) e.Response).StatusCode.GetHashCode()} {((HttpWebResponse) e.Response).StatusDescription}";
                invalidLinksFile.WriteLine(message);
            }
        }

        public static HashSet<string> GetLinksFromSite(string link)
        {
            var web = new HtmlWeb();
            var document = web.Load(link);

            return document.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(IsLinkCorrect)
                .ToHashSet();
        }

        private static void GetUniqueLinksFromSite(string link, List<string> links)
        {
            links.Add(link);
            if (IsContainProtocol(link)) return;

            var linksFromPage = GetLinksFromSite(FormFullLink(link));

            if (!linksFromPage.Any()) return;
            foreach (var str in linksFromPage.Where(l => !links.Contains(l)))
                GetUniqueLinksFromSite(str, links);
        }

        private static IEnumerable<string> GetLinks()
        {
            var links = new List<string>();
            var link = string.Empty;

            GetUniqueLinksFromSite(link, links);

            return links;
        }

        private static void Main()
        {
            using var invalidLinksFile = new StreamWriter("../../../invalid.txt");
            using var validLinksFile = new StreamWriter("../../../valid.txt");
            var links = GetLinks();

            foreach (var link in links) WriteLinks(link, invalidLinksFile, validLinksFile);
        }
    }
}