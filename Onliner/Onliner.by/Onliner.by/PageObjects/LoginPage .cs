using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Onliner.PageObjects
{
    public class LoginPage
    {
        private readonly By _passwordLocator = By.CssSelector("div[class='auth-box__line']>input[type='password']");
        private readonly By _usernameLocator = By.CssSelector("div[class='auth-box__line']>input[type='text']");
        private readonly By _submitLocator = By.ClassName("auth-box__auth-submit");
        private readonly IWebDriver _driver;
        private IWebElement _inputUserName;
        private IWebElement _inputPassword;
        private IWebElement _buttonSubmit;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoginPage EnterUsername(string username)
        {
            _inputUserName = _driver.FindElement(_usernameLocator);
            _inputUserName.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            _inputPassword = _driver.FindElement(_passwordLocator);
            _inputPassword.SendKeys(password);
            return this;
        }

        public void SubmitLogin()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(45));
            _buttonSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(_submitLocator));
            var actions = new Actions(_driver);
            actions.MoveToElement(_buttonSubmit).Click().Build().Perform();
        }
    }
}
