using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.IO;
using HtmlAgilityPack;

namespace LinksChecker
{
    class Program
    {
        const string protocol = "http";
        const string url = "http://91.210.252.240/broken-links/";

        private static bool IsContainProtocol(string link)
        {
            return link.Contains(protocol);
        }

        private static bool IsLinkCorrect(string link)
        {
            return !string.IsNullOrEmpty(link) && !link.StartsWith("#");
        }

        private static string FormFullLink(string link)
        {
            return IsContainProtocol(link) ? link : url + link;
        }

        private static void WriteLinks(string link, StreamWriter invalidLinksFile, StreamWriter validLinksFile)
        {
            string fullLink = FormFullLink(link);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullLink);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string message = $"{fullLink} {response.StatusCode.GetHashCode()} {response.StatusDescription}";
                validLinksFile.WriteLine(message);
            }
            catch (WebException e)
            {
                string message = $"{fullLink} {((HttpWebResponse)e.Response).StatusCode.GetHashCode()} {((HttpWebResponse)e.Response).StatusDescription}";
                invalidLinksFile.WriteLine(message);
            }
        }

        public static HashSet<string> GetLinksFromSite(string link)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(link);

            return document.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(url => IsLinkCorrect(url))
                .ToHashSet();
        }

        private static void GetUniqueLinksFromSite(string link, List<string> links)
        {
            links.Add(link);
            if (IsContainProtocol(link))
            {
                return;
            }

            HashSet<string> linksFromPage = GetLinksFromSite(FormFullLink(link));

            if (linksFromPage.Any())
            {
                foreach (string str in linksFromPage.Where(l => !links.Contains(l)))
                {
                    GetUniqueLinksFromSite(str, links);
                }
            }
        }

        public static List<string> GetLinks()
        {
            List<string> links = new List<string>();
            string link = string.Empty;

            GetUniqueLinksFromSite(link, links);

            return links;
        }

        static void Main()
        {
            using StreamWriter invalidLinksFile = new StreamWriter("../../../invalid.txt");
            using StreamWriter validLinksFile = new StreamWriter("../../../valid.txt");
            var links = GetLinks();

            foreach (string link in links)
            {
                WriteLinks(link, invalidLinksFile, validLinksFile);
            }
        }
    }
}