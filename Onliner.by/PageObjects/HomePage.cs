using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Onliner.Configurations;
using Onliner.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Onliner.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private Random random = new Random();
        private IWebElement _buttonSignIn;
        private IWebElement _profileMenu;
        private IWebElement _buttonLogOut;
        private const string RegularFindOpinions = @"""b-opinions-main-2__text"">(.+)(?=[<])";
        private readonly By _userNameLocator = By.XPath("//div[contains(@class,'b-top-profile__name')]/a[contains(@href,'profile')]");
        private readonly By _profileMenuLocator = By.XPath("//div[@id='userbar']//div/a[contains(@href,'profile')]");
        private readonly By _signInLocator = By.XPath("//div[contains(@class,'auth-bar')]/div[contains(@class,'auth-bar__item--text')]");
        private readonly By _logoutLocator = By.XPath("//div[contains(@class,'b-top-profile__logout')]/a[contains(@class,'b-top-profile__link')]");
        private readonly By _mainPageLocator =
            By.XPath("//div[contains(@class,'b-main-page-grid-4')]/header[contains(@class,'b-main-page-blocks-header-2')]");
        private readonly By _topicsLocator =
            By.XPath("//div[contains(@class,'project-navigation__flex')]//li[contains(@class,'project-navigation__item')]");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public HomePage NavigateHomePage()
        {
            _driver.Navigate().GoToUrl(SettingsSection.Settings.Url);
            return this;
        }

        public HomePage ClickSignIn()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(SettingsSection.Settings.ExplicitWait));
            _buttonSignIn = wait.Until(ExpectedConditions.ElementToBeClickable(_signInLocator));
            _buttonSignIn.Click();
            return this;
        }

        public IWebElement GetRandomTopicFromList()
        {
            var listTopics = _driver.FindElements(_topicsLocator);
            var numberTopic = random.Next(0, listTopics.Count);
            var randomTopic = listTopics.ToArray()[numberTopic].FindElement(By.TagName("a"));
            return randomTopic;
        }

        public TopicPage NavigateToRandomTopic(IWebElement topic)
        {
            _driver.Navigate().GoToUrl(topic.GetAttribute("href"));
            return new TopicPage(_driver);
        }

        public string GetUserName()
        {
            var actions = new Actions(_driver);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(SettingsSection.Settings.ExplicitWait));
            _profileMenu = wait.Until(ExpectedConditions.ElementToBeClickable(_profileMenuLocator));
            actions.MoveToElement(_profileMenu).Click().Build().Perform();
            var username = wait.Until(ExpectedConditions.ElementIsVisible(_userNameLocator)).Text;
            actions.MoveToElement(_profileMenu).Perform();
            return username;
        }

        public int NumberVerifiableEntity(string verifiableEntity)
        {
            By locator = null;
            switch (verifiableEntity)
            {
                case "CurrentIsMainPage":
                {
                    locator = _mainPageLocator;
                }
                    break;

                case "LogoutUser":
                {
                    locator = _signInLocator;
                }
                    break;

                default:
                {
                    throw new Exception("Locator is null.");
                }
            }
            var profile = _driver.FindElements(locator);
            return profile.Count;
        }

        public void WriteOpinions()
        {
            var pageSource = _driver.PageSource;
            var opinions = new List<string>();
            foreach (Match match in Regex.Matches(pageSource, RegularFindOpinions, RegexOptions.IgnoreCase))
            {
                opinions.Add(match.Groups[1].Value);
            }
            var pathToFile = Environment.CurrentDirectory + SettingsSection.Settings.PathToFile + "\\";
            FileService.WriteFileCsv(opinions, pathToFile, SettingsSection.Settings.NameFile);
        }

        public LoginPage Logout()
        {
            var actions = new Actions(_driver);
            _profileMenu = _driver.FindElement(_profileMenuLocator);
            actions.MoveToElement(_profileMenu).Click().Build().Perform();
            _buttonLogOut = _driver.FindElement(_logoutLocator);
            _buttonLogOut.Click();
            return new LoginPage(_driver);
        }
    }
}