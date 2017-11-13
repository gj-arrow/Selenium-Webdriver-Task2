using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Task2
{
    public class LoginPage
    {
        private By usernameLocator = By.CssSelector("#auth-container__forms > div > div.auth-box__field > form > div:nth-child(1) > div:nth-child(1) > input");
        private By passwordLocator = By.CssSelector("#auth-container__forms > div > div.auth-box__field > form > div:nth-child(1) > div:nth-child(2) > input");
        private By submitLocator = By.CssSelector("#auth-container__forms > div > div.auth-box__field > form > div:nth-child(3) > div > button");
        private IWebDriver driver;
        private IWebElement inputUserName;
        private IWebElement inputPassword;
        private IWebElement buttonSubmit;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LoginPage EnterUsername(string username)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            inputUserName = wait.Until(ExpectedConditions.ElementIsVisible(usernameLocator));
            //Func<IWebDriver, IWebElement> waitForElement = ((web) =>
            //{
            //    IWebElement qwe = driver.FindElement(usernameLocator);
            //    return qwe;
            //});
            //inputUserName = wait.Until(waitForElement);
            inputUserName.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            inputPassword = wait.Until(ExpectedConditions.ElementIsVisible(passwordLocator));
           // inputPassword = driver.FindElement(passwordLocator);
            inputPassword.SendKeys(password);
            return this;
        }

        public void SubmitLogin()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            buttonSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(submitLocator));
            Actions actions = new Actions(driver);
            actions.MoveToElement(buttonSubmit).Click().Build().Perform();
            //buttonSubmit.Click();
        }
    }
}
