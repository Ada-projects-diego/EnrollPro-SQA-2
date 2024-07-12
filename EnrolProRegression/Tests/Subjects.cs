using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Xunit;

namespace StudentEnrolmentRegression.Tests
{
    public class Subjects : DefactBaseTests
    {
        [Fact]
        public void SubjectRegTest()
        {
            ExecuteTest(() =>
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--disable-gpu");
                chromeOptions.AddArgument("--window-size=1280x1024");

                _driver = new ChromeDriver(chromeOptions);
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                _driver.Navigate().GoToUrl("http://localhost:4200/");
                Console.WriteLine("I got past the front end");

                _driver.FindElement(By.LinkText("Subjects")).Click();
                Console.WriteLine("At the subjects page");
                _wait.Until(drv => drv.FindElement(By.Id("subject-table")).FindElements(By.TagName("tr")).Count > 1);
                Console.WriteLine("Able to load the data");
                var table = _driver.FindElement(By.Id("subject-table"));
                var rows = table.FindElements(By.TagName("tr"));
                AssertTableRowCount(table, 1);

                AddEntity("Add Subject", "Enter subject name", "Enter subject description", "Calculus", "A study of change and motion using derivatives and integrals.");

                AssertTableRowCount(table, 2);
                DeleteEntityByName(table, "Calculus", "A study of change and motion using derivatives and integrals.", "delete-subject");
                AssertTableRowCount(table, 1);
                Dispose();
            });
        }
    }
}
