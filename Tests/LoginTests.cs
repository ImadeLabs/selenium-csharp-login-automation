using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharpLoginAutomation.Pages;
using SeleniumCSharpLoginAutomation.Utilities;
using System.Threading;

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
            Thread.Sleep(2000);

            loginPage.EnterUsername("tomsmith");
            Thread.Sleep(2000);

            loginPage.EnterPassword("SuperSecretPassword!");
            Thread.Sleep(2000);

            loginPage.ClickLogin();
            Thread.Sleep(5000);

            Assert.That(driver!.Url, Does.Contain("/secure"));
        }

        [Test]
        public void InvalidLoginTest()
        {
            loginPage!.Open();
            Thread.Sleep(2000);

            loginPage.EnterUsername("wrong_user");
            Thread.Sleep(2000);

            loginPage.EnterPassword("wrong_password");
            Thread.Sleep(2000);

            loginPage.ClickLogin();
            Thread.Sleep(5000);

            Assert.That(loginPage.GetFlashMessage(), Does.Contain("Your username is invalid!"));
        }

        [Test]
        public void EmptyLoginTest()
        {
            loginPage!.Open();
            Thread.Sleep(2000);

            loginPage.ClickLogin();
            Thread.Sleep(5000);

            Assert.That(loginPage.GetFlashMessage(), Does.Contain("Your username is invalid!"));
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                Thread.Sleep(5000);
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}