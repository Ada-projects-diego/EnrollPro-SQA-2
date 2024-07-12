using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Xunit;

namespace StudentEnrolmentRegression.Tests
{
    public class Courses : DefactBaseTests
    {
        [Fact]
        public void CourseRegTest()
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

                _driver.FindElement(By.LinkText("Courses")).Click();

                _wait.Until(drv => drv.FindElement(By.Id("course-table")).FindElements(By.TagName("tr")).Count > 1);

                var table = _driver.FindElement(By.Id("course-table"));
                var rows = table.FindElements(By.TagName("tr"));
                AssertTableRowCount(table, 1);

                AddEntity("Add Course", "Enter course name", "Enter course description", "English", "English 101");
                // Wait until the modal with the ID "myModal" is no longer visible

                AssertTableRowCount(table, 2);
                DeleteEntityByName(table, "English", "English 101", "delete-course");
                AssertTableRowCount(table, 1);
                Dispose();
            });
        }
    }
}
