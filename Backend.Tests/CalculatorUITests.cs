//selenium and CI

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Backend.Tests
{
    [TestFixture]
    public class CalculatorUITests
    {
        private IWebDriver? _driver;
        private WebDriverWait? _wait;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            if (Environment.GetEnvironmentVariable("CI") == "true")
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
            }

            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void AddTwoNumbers_UI() => RunCalculatorTest("5", "3", "add", "Result: 8");
        [Test]
        public void SubtractTwoNumbers_UI() => RunCalculatorTest("10", "4", "subtract", "Result: 6");

        private void RunCalculatorTest(string num1, string num2, string operation, string expectedResult)
        {
            if (_driver == null || _wait == null)
                Assert.Fail("WebDriver not initialized.");

            _driver.Navigate().GoToUrl("http://localhost:5173");

            _driver.FindElement(By.CssSelector("input[placeholder='First Number']")).Clear();
            _driver.FindElement(By.CssSelector("input[placeholder='First Number']")).SendKeys(num1);

            _driver.FindElement(By.CssSelector("input[placeholder='Second Number']")).Clear();
            _driver.FindElement(By.CssSelector("input[placeholder='Second Number']")).SendKeys(num2);

            var select = new SelectElement(_driver.FindElement(By.TagName("select")));
            select.SelectByValue(operation.ToLower());

            _driver.FindElement(By.TagName("button")).Click();

            var resultElement = _wait.Until(d => d.FindElement(By.TagName("h2")));
            Assert.AreEqual(expectedResult, resultElement.Text);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}


