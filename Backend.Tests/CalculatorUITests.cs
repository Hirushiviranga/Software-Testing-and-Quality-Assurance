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
            // Remove headless to see the browser UI
            // options.AddArgument("--headless");

            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void AddTwoNumbers_UI()
        {
            RunCalculatorTest("5", "3", "add", "Result: 8");
        }

        [Test]
        public void SubtractTwoNumbers_UI()
        {
            RunCalculatorTest("10", "4", "subtract", "Result: 6");
        }

        [Test]
        public void MultiplyTwoNumbers_UI()
        {
            RunCalculatorTest("6", "7", "multiply", "Result: 42");
        }

        [Test]
        public void DivideTwoNumbers_UI()
        {
            RunCalculatorTest("20", "4", "divide", "Result: 5");
        }

        private void RunCalculatorTest(string num1, string num2, string operation, string expectedResult)
        {
            if (_driver == null || _wait == null)
                Assert.Fail("WebDriver not initialized.");

            _driver.Navigate().GoToUrl("http://localhost:5173");

            _driver.FindElement(By.CssSelector("input[placeholder='First Number']")).SendKeys(num1);
            _driver.FindElement(By.CssSelector("input[placeholder='Second Number']")).SendKeys(num2);

            var select = _driver.FindElement(By.TagName("select"));
            select.FindElement(By.CssSelector($"option[value='{operation.ToLower()}']")).Click();

            _driver.FindElement(By.TagName("button")).Click();

            var resultElement = _wait.Until(d => d.FindElement(By.TagName("h2")));
            var result = resultElement.Text;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void HistoryPage_UI()
        {
            if (_driver == null || _wait == null)
                Assert.Fail("WebDriver not initialized.");

            _driver.Navigate().GoToUrl("http://localhost:5173");

            // Perform a calculation
            _driver.FindElement(By.CssSelector("input[placeholder='First Number']")).SendKeys("2");
            _driver.FindElement(By.CssSelector("input[placeholder='Second Number']")).SendKeys("3");
            _driver.FindElement(By.TagName("button")).Click();

            // Wait until the result is displayed
            _wait.Until(d => d.FindElement(By.TagName("h2")).Text.Contains("5"));

            // Navigate to History
            _driver.FindElement(By.LinkText("Go To History")).Click();

            // Wait longer for history items
            var waitLong = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            var historyItem = waitLong.Until(d =>
            {
                var items = d.FindElements(By.CssSelector("[data-testid^='history-']"));
                foreach (var li in items)
                {
                    if (li.Text.Replace(" ", "").Contains("2+3=5"))
                        return li;
                }
                return null;
            });

            Assert.IsNotNull(historyItem, "Calculation 2+3=5 not found in history.");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _driver = null;
            _wait = null;
        }
    }
}



