using System;
using System.Threading;
using OpenQA.Selenium;

namespace Task2
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement buttonAuth;
        private IWebElement profileMenu;
        private IWebElement buttonLogOut;


        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoHomePage()
        {
            _driver.Navigate().GoToUrl("https://www.onliner.by/");
        }

        public void ClickAuth()
        {
            buttonAuth = _driver.FindElement(By.XPath("//*[@id='userbar']/div/div[1]"));
            if (buttonAuth.Enabled)
            {
                buttonAuth.Click();
            }
        }

        public void Logout()
        {
            profileMenu = _driver.FindElement(By.XPath("//*[@id='userbar']/div[1]/div[1]/a"));
            if (profileMenu.Enabled)
            {
                profileMenu.Click();
            }
            buttonLogOut = _driver.FindElement(By.XPath("//*[@id='userbar']/div[1]/div[1]/div/div[1]/div[1]/div[2]/div/a"));
            if (buttonLogOut.Enabled)
            {
                buttonLogOut.Click();
            }
        }
    }
}