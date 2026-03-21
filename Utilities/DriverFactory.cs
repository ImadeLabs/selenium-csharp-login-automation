using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCSharpLoginAutomation.Utilities
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}