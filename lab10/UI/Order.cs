using System.Threading;
using OpenQA.Selenium;

namespace UI
{
    public static class Order
    {
        internal static string[] AddToCart(IWebDriver webDriver)
        {
            var mainPageLink = By.XPath("/html/body/div[2]/a");
            var mainPage = webDriver.FindElement(mainPageLink);
            mainPage.Click();
            var productLink = By.XPath("/html/body/div[4]/div[4]/div/div/div/div[1]/div/div[1]/h3/a");
            var product = webDriver.FindElement(productLink);
            product.Click();
            var addToCartBtn = By.XPath("/html/body/div[4]/div[3]/div/div/div[1]/div[1]/div[2]/div/a");
            var addToCart = webDriver.FindElement(addToCartBtn);
            addToCart.Click();
            Thread.Sleep(2000);
            var orderNameLink = By.XPath("/html/body/div[7]/div/div/div[2]/div/table/tbody/tr[1]/td[2]/a");
            var orderNameText = webDriver.FindElement(orderNameLink).Text;
            var totalPrice = By.XPath("//*[@id='cart']/div/div/div[2]/div/table/tbody/tr[3]/td[2]");
            var totalPriceText = webDriver.FindElement(totalPrice).Text;
            var continueShoppingBtn = By.XPath("/html/body/div[7]/div/div/div[3]/button[1]");
            var continueShopping = webDriver.FindElement(continueShoppingBtn);
            continueShopping.Click();
            var cartPriceSpan = By.XPath("/html/body/div[1]/div/div/div[2]/div/a/div/span");
            var cartPriceText = webDriver.FindElement(cartPriceSpan).Text;

            return new[] {orderNameText, totalPriceText, cartPriceText};
        }

        internal static string[] MakeOrder(IWebDriver webDriver)
        {
            Thread.Sleep(1000);
            var openCartBtn = By.XPath("/html/body/div[1]/div/div/div[2]/div/a");
            var openCart = webDriver.FindElement(openCartBtn);
            openCart.Click();
            Thread.Sleep(2000);
            var makeOrderBtn = By.XPath("/html/body/div[7]/div/div/div[3]/a");
            var makeOrder = webDriver.FindElement(makeOrderBtn);
            makeOrder.Click();
            var orderNameLink = By.XPath("/html/body/div[4]/div[3]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/a");
            var orderNameText = webDriver.FindElement(orderNameLink).Text;
            var totalPrice = By.XPath("/html/body/div[4]/div[3]/div/div/div/div/div[2]/table/tbody/tr[3]/td[2]");
            var totalPriceText = webDriver.FindElement(totalPrice).Text;
            var noteTextArea = By.XPath("/html/body/div[4]/div[3]/div/div/div/div/div[3]/form/div/textarea");
            var note = webDriver.FindElement(noteTextArea);
            note.SendKeys("подарочек положите");
            var orderBtn = By.XPath("/html/body/div[4]/div[3]/div/div/div/div/div[3]/form/button");
            var order = webDriver.FindElement(orderBtn);
            order.Click();
            var successMessageDiv = By.XPath("/html/body/div[4]/div[1]/div/div/div");
            var successesMessage = webDriver.FindElement(successMessageDiv).Text;

            return new[] {orderNameText, totalPriceText, successesMessage};
        }
    }
}