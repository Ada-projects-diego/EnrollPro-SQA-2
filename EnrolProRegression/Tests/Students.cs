using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Xunit;

namespace StudentEnrolmentRegression.Tests
{
    public class Students : DefactBaseTests
    {
        [Fact]
        public void StudentRegTest()
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

                _driver.FindElement(By.LinkText("Students")).Click();

                _wait.Until(drv => drv.FindElement(By.Id("student-table")).FindElements(By.TagName("tr")).Count > 1);

                var table = _driver.FindElement(By.Id("student-table"));
                var rows = table.FindElements(By.TagName("tr"));
                AssertTableRowCount(table, 1);

                AddEntity("Add Student", "Enter student name", "Enter student surname", "Diego", "Jaumandreu Ondaro");

                AssertTableRowCount(table, 2);
                DeleteEntityByName(table, "Diego", "Jaumandreu Ondaro", "delete-student");
                AssertTableRowCount(table, 1);
                Dispose();
            });
        }
    }
}
