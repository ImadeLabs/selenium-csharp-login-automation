using OpenQA.Selenium;

namespace SeleniumCSharpLoginAutomation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private readonly By UsernameInput = By.Id("username");
        private readonly By PasswordInput = By.Id("password");
        private readonly By LoginButton = By.CssSelector("button[type='submit']");
        private readonly By FlashMessage = By.Id("flash");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        }

        public void EnterUsername(string username)
        {
            var usernameField = _driver.FindElement(UsernameInput);
            usernameField.Clear();
            usernameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var passwordField = _driver.FindElement(PasswordInput);
            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            _driver.FindElement(LoginButton).Click();
        }

        public string GetFlashMessage()
        {
            return _driver.FindElement(FlashMessage).Text;
        }
    }
}