using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Onliner.Configurations;
using Onliner.PageObjects;
using Onliner.BrowserFactory;

namespace OnlinerTest
{
    [TestFixture]
    public class TestOnliner
    {
        private IWebDriver _driver;
        private string _actualTopicName;
        private string _expectedTopicName;
        private HomePage _homePage;
        private LoginPage _loginPage;
        private readonly By _signInLocator = By.XPath("//div[@id='userbar']//div[contains(text(),'Вход')]");
        private readonly By _profileLocator = By.XPath("//div[@id='userbar']//div/a[contains(@href,'profile')]");
        private readonly By _mainPageLocator =
            By.XPath("//div[@class='b-main-page-grid-4 b-main-page-news-2']/header[@class='b-main-page-blocks-header-2 cfix']");

        [SetUp]
        public void Initialize()
        {
            BrowserFactory.InitBrowser(SettingsSection.Settings.Browser);
            _driver = BrowserFactory.Driver;
            if (SettingsSection.Settings.Browser == "chrome")
            {
                _driver.Manage().Window.Maximize();
            }
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(SettingsSection.Settings.ImplicitWait);
            _homePage = new HomePage(_driver);
            _loginPage = new LoginPage(_driver);
        }

        [TearDown]
        public void Dispose()
        {
            BrowserFactory.CloseDrivers();
        }

        [Test]
        public void AutoTestOnliner()
        {
            _homePage.NavigateHomePage();
            var numberSectionTopics = _homePage.FindNumberOfElements(_mainPageLocator);
            Assert.IsTrue(numberSectionTopics == 6);
            _homePage.ClickSignIn();
            Authorization(_loginPage);
            var profileElements = _homePage.FindNumberOfElements(_profileLocator);
            Assert.IsFalse(profileElements == 0);
            CheckRandomTopic();
            Assert.AreEqual(_expectedTopicName, _actualTopicName);
            _homePage.NavigateHomePage();
            _homePage.WriteOpinionsInCsv();
            _homePage.Logout();
            var signIneElements = _homePage.FindNumberOfElements(_signInLocator);
            Assert.IsTrue(signIneElements == 1);
        }

        private static void Authorization(LoginPage logPage)
        {
            logPage.EnterUsername(SettingsSection.Settings.Username);
            logPage.EnterPassword(SettingsSection.Settings.Password);
            logPage.SubmitLogin();
        }

        private void CheckRandomTopic()
        {
            var displayed = false;
            while (!displayed)
            {
                var topicElement = _homePage.GetRandomTopicFromList();
                if (topicElement.Displayed)
                {
                    displayed = true;
                    _expectedTopicName = topicElement.Text;
                    var topicPage = _homePage.NavigateToRandomTopic(topicElement);
                    _actualTopicName = topicPage.GetNameOfTopic();
                }
            }
        }
    }
}