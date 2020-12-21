using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UI
{
    public class UiTests
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://91.210.252.240:9000");
        }

        [Test]
        [Order(0)]
        public void AuthorizationTest()
        {
            const string expected = "Вы успешно авторизованы";
            Assert.AreEqual(expected, Auth.Authorize(_driver));
        }

        [Test]
        [Order(1)]
        public void AddToCartTest()
        {
            var expected = new[]
            {
                "Casio MRP-700-1AVEF",
                "$300",
                "$300"
            };
            Assert.AreEqual(expected, Order.AddToCart(_driver));
        }

        [Test]
        [Order(2)]
        public void MakeOrderTest()
        {
            var expected = new[]
            {
                "Casio MRP-700-1AVEF", "$300",
                "Спасибо за Ваш заказ. В ближайшее время с Вами свяжется менеджер для согласования заказа"
            };
            Assert.AreEqual(expected, Order.MakeOrder(_driver));
        }

        [Test]
        [Order(3)]
        public void SearchTest()
        {
            var expected = new[]
            {
                "Поиск по запросу \"Casio\"",
                "47"
            };
            Assert.AreEqual(expected, Search.SearchProduct(_driver));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}