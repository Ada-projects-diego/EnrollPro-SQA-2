using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace StudentEnrolmentRegression.Tests
{
    public abstract class DefactBaseTests
    {
        public IWebDriver? _driver;
        public WebDriverWait? _wait;
        private static readonly HttpClient HttpClient = new HttpClient();
        protected string? PageSourceHtml { get; set; }
        private const int MaxRetries = 0;

        protected void ExecuteTest(Action testLogic, [CallerMemberName] string testName = "")
        {
            Console.WriteLine("Inside execute test");
            try
            {
                for (int retry = 0; retry <= MaxRetries; retry++)
                {
                    try
                    {
                        Console.WriteLine("Inside the try (test logic can be run");
                        testLogic();
                        return; // If successful, it will exit the function.
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error thrown after trying to run test Logic. The error is: ");
                        Console.WriteLine(ex.ToString());
                        throw ex;
                    }
                }
            }
            catch (Exception testFailedException)
            {
                CapturePageSource();
                Dispose();
            }
        }
        protected void CapturePageSource()
        {
            try
            {
                if (_driver != null)
                {
                    PageSourceHtml = _driver.PageSource;
                }
            }
            catch (Exception ex)
            {
                // You might want to log this exception or throw it
                Console.WriteLine($"Failed to capture page source: {ex.Message}");
            }
        }
        protected internal void AddEntity(string buttonName, string placeholder1, string placeholder2, string value1, string value2)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//button[normalize-space()='{buttonName}']"))).Click();
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//input[@placeholder='{placeholder1}']"))).SendKeys(value1);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//input[@placeholder='{placeholder2}']"))).SendKeys(value2);
            _driver.FindElement(By.XPath("//button[normalize-space()='Add']")).Click();
        }
        protected internal void AssertTableRowCount(IWebElement table, int expectedRowCount)
        {
            _wait.Until(drv => table.FindElements(By.TagName("tr")).Count - 1 == expectedRowCount);
            var rows = table.FindElements(By.TagName("tr")).Count;
            rows--;
            Assert.Equal(expectedRowCount, rows);
        }
        protected internal void DeleteEntityByName(IWebElement table, string firstName, string surname, string deleteButtonId)
        {
            var rows = table.FindElements(By.TagName("tr")).Skip(1);

            foreach (var row in rows)
            {
                var cols = row.FindElements(By.TagName("td")).ToList();

                if (cols[1].Text.Equals(firstName) && cols[2].Text.Equals(surname))
                {
                    var deleteButton = row.FindElement(By.Id(deleteButtonId));
                    deleteButton.Click();
                    return;
                }
            }
            throw new Exception($"Entity with the name {firstName} {surname} not found in the table.");
        }
        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null; 
            }
        }
    }
}