using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Configuration;
using Onliner.Configurations;
using Onliner.PageObjects;
using Onliner.BrowserFactory;
using System.Collections.Generic;
using System.Linq;

namespace OnlinerTest
{
    [TestFixture]
    public class TestOnliner
    {
        private IWebDriver _driver;
        private CustomSettings _settings;
        private HomePage _homePage;
        private LoginPage _loginPage;
        private readonly By _signInLocator = By.XPath("//div[@id='userbar']//div[contains(text(),'Вход')]");
        private readonly By _profileLocator = By.XPath("//div[@id='userbar']//div/a[contains(@href,'profile')]");
        private const string SectionAppConfig = "Settings";

        [SetUp]
        public void Initialize()
        {
            try
            {
                var config = (CustomSettingsSection) ConfigurationManager.GetSection(SectionAppConfig);
                _settings = config.TakeSettingsFromConfig();
                BrowserFactory.InitBrowser(_settings.Browser);
                _driver = BrowserFactory.Driver;
                if (_settings.Browser == "Chrome")
                {
                    _driver.Manage().Window.Maximize();
                }
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TearDown]
        public void Dispose()
        {
            BrowserFactory.CloseDrivers();
        }

        [Test]
        public void AutoTestOnliner()
        {
            _homePage = new HomePage(_driver, _settings.Url);
            _loginPage = new LoginPage(_driver);
            _homePage.NavigateHomePage();
            Assert.AreEqual("Onliner.by", _driver.Title);
            _homePage.ClickSignIn();
            Authorization(_loginPage);
            var profileElements = _homePage.FindNumberOfElements(_profileLocator);
            Assert.IsFalse(profileElements == 0);
            var resultRandomTopic = CheckRandomTopic();
            Assert.AreEqual(resultRandomTopic["expectedTopic"],resultRandomTopic["actualTopic"]);
            _homePage.NavigateHomePage();
            _homePage.WriteAllOpinionInCsv();
            _homePage.Logout();
            var signIneElements = _homePage.FindNumberOfElements(_signInLocator);
            Assert.IsTrue(signIneElements == 1);
        }

        private static void Authorization(LoginPage logPage)
        {
            logPage.EnterUsername("gj-arrow");
            logPage.EnterPassword("123456789Qwerty");
            logPage.SubmitLogin();
        }

        private Dictionary<string,string> CheckRandomTopic()
        {
            var displayed = false;
            var result = new Dictionary<string,string>();
            while (!displayed)
            {
                var topic = _homePage.GetRandomTopic();
                if (topic.Displayed)
                {
                    displayed = true;
                    result.Add("expectedTopic",topic.Text);
                    var top = _homePage.NavigateToRandomTopic(topic);
                    result.Add("actualTopic",top.GetNameOfTopic());
                }
            }
            return result;
        }
    }
}