using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Onliner.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Onliner.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private const string FormatFile = ".csv";
        private Random random = new Random();
        private IWebElement _buttonSignIn;
        private IWebElement _profileMenu;
        private IWebElement _buttonLogOut;
        private const string RegularFindOpinions = @"""b-opinions-main-2__text"">(.+)(?=[<])";
        private readonly By _profileLocator = By.XPath("//div[@id='userbar']//div/a[contains(@href,'profile')]");
        private readonly By _signInLocator = By.XPath("//div[@id='userbar']//div[contains(text(),'Вход')]");
        private readonly By _logoutLocator = By.XPath("//div[@id='userbar']//div/a[contains(text(),'Выйти')]");
        private readonly By _topicsLocator =
            By.XPath("//div[@class='project-navigation__flex']//li[@class='project-navigation__item project-navigation__item_secondary']");

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
            var fluentWait = new DefaultWait<IWebDriver>(_driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(30);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(2000);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            _buttonSignIn = fluentWait.Until(ExpectedConditions.ElementToBeClickable(_signInLocator));
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

        public int FindNumberOfElements(By locator)
        {
            var profile = _driver.FindElements(locator);
            return profile.Count;
        }

        public void WriteOpinionsInCsv()
        {
            var pageSource = _driver.PageSource;
            var opinions = new List<string>();
            var pathToFileFull = "";
            foreach (Match match in Regex.Matches(pageSource, RegularFindOpinions, RegexOptions.IgnoreCase))
            {
                opinions.Add(match.Groups[1].Value);
            }
            pathToFileFull = Environment.CurrentDirectory + SettingsSection.Settings.PathToFile + "\\"+ SettingsSection.Settings.NameFile + FormatFile;
            using (var writerToCsv = File.CreateText(pathToFileFull))
            {
                foreach (var opinion in opinions)
                {
                    writerToCsv.WriteLine(opinion);
                }
            }
        }

        public LoginPage Logout()
        {
            var actions = new Actions(_driver);
            _profileMenu = _driver.FindElement(_profileLocator);
            actions.MoveToElement(_profileMenu).Click().Build().Perform();
            _buttonLogOut = _driver.FindElement(_logoutLocator);
            _buttonLogOut.Click();
            return new LoginPage(_driver);
        }
    }
}