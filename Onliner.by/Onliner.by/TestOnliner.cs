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

        [SetUp]
        public void Initialize()
        {
            BrowserFactory.InitBrowser(Config.Browser.ToUpper());
            _driver = BrowserFactory.Driver;
            if (Config.Browser.ToUpper() == "CHROME")
            {
                _driver.Manage().Window.Maximize();
            }
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitWait);
            _homePage = new HomePage(_driver);
            _loginPage = new LoginPage(_driver);
        }

        [TearDown]
        public void Dispose()
        {
            BrowserFactory.CloseDriver();
        }

        [Test]
        public void AutoTestOnliner()
        {
            _homePage.NavigateHomePage();
            var numberSectionsTopic = _homePage.NumberVerifiableEntity("CurrentIsMainPage");
            Assert.IsTrue(numberSectionsTopic == 6, "The main page should be divided into 6 sections according to the topics.");
            _homePage.ClickSignIn();
            Authorization(_loginPage);
            var userName = _homePage.GetUserName();
            Assert.AreEqual(Config.Username, userName, "User names must match");
            CheckRandomTopic();
            Assert.AreEqual(_expectedTopicName, _actualTopicName, "Topic names must match");
            _homePage.NavigateHomePage();
            _homePage.WriteOpinions();
            _homePage.Logout();
            var signIneElements = _homePage.NumberVerifiableEntity("LogoutUser");
            Assert.IsTrue(signIneElements == 1, "User has not been logged out.Button 'Вход' must be on page");
        }

        private static void Authorization(LoginPage logPage)
        {
            logPage.EnterUsername(Config.Username);
            logPage.EnterPassword(Config.Password);
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