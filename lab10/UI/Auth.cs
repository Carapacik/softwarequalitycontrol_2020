using OpenQA.Selenium;

namespace UI
{
    public static class Auth
    {
        public static string Authorize(IWebDriver webDriver)
        {
            var accountButton = By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/a");
            var account = webDriver.FindElement(accountButton);
            account.Click();
            var signInButton = By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/ul/li[1]/a");
            var signin = webDriver.FindElement(signInButton);
            signin.Click();
            var loginInput = By.XPath("//*[@name='login']");
            var login = webDriver.FindElement(loginInput);
            login.SendKeys("Login");
            var passwordInput = By.XPath("//*[@name='password']");
            var password = webDriver.FindElement(passwordInput);
            password.SendKeys("Password");
            var orderButton = By.XPath("/html/body/div[4]/div[3]/div/div/div/div/div[2]/div/form/button");
            var order = webDriver.FindElement(orderButton);
            order.Click();
            var successMessageDiv = By.XPath("/html/body/div[4]/div[1]/div/div/div");

            return webDriver.FindElement(successMessageDiv).Text;
        }
    }
}