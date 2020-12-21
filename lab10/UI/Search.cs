using System.Collections.Generic;
using OpenQA.Selenium;

namespace UI
{
    public static class Search
    {
        internal static string[] SearchProduct(IWebDriver webDriver)
        {
            var mainPageLink = By.XPath("/html/body/div[2]/a");
            var mainPage = webDriver.FindElement(mainPageLink);
            mainPage.Click();
            var searchInput = By.XPath("//*[@id='typeahead']");
            var search = webDriver.FindElement(searchInput);
            search.SendKeys("Casio");
            var searchBtn = By.XPath("/html/body/div[3]/div/div/div[2]/div/form/input");
            var searchStart = webDriver.FindElement(searchBtn);
            searchStart.Click();
            var searchLi = By.XPath("/html/body/div[4]/div[2]/div/div/ol/li[2]");
            var searchLiText = webDriver.FindElement(searchLi).Text;
            var foundList = new List<IWebElement>();
            try
            {
                for (var index = 0; index < int.MaxValue; index++)
                    foundList.Add(
                        webDriver.FindElement(
                            By.XPath($"/html/body/div[4]/div[3]/div/div/div[1]/div/div[{index + 1}]")));

                return new[] {searchLiText, foundList.Count.ToString()};
            }
            catch
            {
                return new[] {searchLiText, foundList.Count.ToString()};
            }
        }
    }
}