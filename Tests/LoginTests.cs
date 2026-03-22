using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharpLoginAutomation.Pages;
using SeleniumCSharpLoginAutomation.Utilities;
using System;
using System.IO;

namespace SeleniumCSharpLoginAutomation.Tests
{
    public class LoginTests
    {
        private IWebDriver? driver;
        private LoginPage? loginPage;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver();
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void ValidLoginTest()
        {
            loginPage!.Open();

            loginPage.EnterUsername("tomsmith");
            loginPage.EnterPassword("SuperSecretPassword!");
            loginPage.ClickLogin();

            Assert.That(driver!.Url, Does.Contain("/secure"));
        }

        [Test]
        public void InvalidLoginTest()
        {
            loginPage!.Open();

            loginPage.EnterUsername("wrong_user");
            loginPage.EnterPassword("wrong_password");
            loginPage.ClickLogin();

            Assert.That(loginPage.GetFlashMessage(), Does.Contain("Your username is invalid!"));
        }

        [Test]
        public void EmptyLoginTest()
        {
            loginPage!.Open();

            loginPage.ClickLogin();

            Assert.That(loginPage.GetFlashMessage(), Does.Contain("Your username is invalid!"));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    TakeScreenshot(TestContext.CurrentContext.Test.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot capture failed: {ex.Message}");
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                }
            }
        }

        private void TakeScreenshot(string testName)
        {
            if (driver is ITakesScreenshot screenshotDriver)
            {
                string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Screenshots", "Failures");
                Directory.CreateDirectory(screenshotsDir);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string safeTestName = testName.Replace(" ", "_");
                string filePath = Path.Combine(screenshotsDir, $"{safeTestName}_{timestamp}.png");

                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"Screenshot saved: {filePath}");
            }
        }
    }
}