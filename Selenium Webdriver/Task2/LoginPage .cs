using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Task2
{
    public class LoginPage
    {
        private By usernameLocator = By.CssSelector("#auth-container__forms > div > div.auth-box__field > form > div:nth-child(1) > div:nth-child(1) > input");
        private By passwordLocator = By.CssSelector("#auth-container__forms > div > div.auth-box__field > form > div:nth-child(1) > div:nth-child(2) > input");
        private IWebDriver driver;
        private IWebElement inputUserName;
        private IWebElement inputPassword;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LoginPage EnterUsername(string username)
        {
            inputUserName = driver.FindElement(usernameLocator);
            if (inputUserName.Enabled)
            {
                inputUserName.SendKeys(username);
            }
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            inputPassword = driver.FindElement(passwordLocator);
            if (inputPassword.Enabled)
            {
                inputPassword.SendKeys(password);
            }
            return this;
        }

        public HomePage SubmitLogin()
        {
            inputPassword.Submit();
            return new HomePage(driver);
        }
    }
}
