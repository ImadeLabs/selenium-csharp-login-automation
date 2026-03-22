using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharpLoginAutomation.Pages;
using SeleniumCSharpLoginAutomation.Utilities;

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
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}