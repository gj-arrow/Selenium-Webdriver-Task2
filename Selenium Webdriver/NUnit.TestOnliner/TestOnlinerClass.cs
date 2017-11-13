using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task2;
using System.Threading;
using System;
using System.Net;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace NUnit.TestOnliner
{
    //public class SingletonWebDriverChrome
  //  {
    //    static readonly IWebDriver instance = new FirefoxDriver();
    //    private IWebDriver driver;

    //    private SingletonWebDriverChrome()
    //    {
    //    }
    //    public static IWebDriver Instance
    //    {
    //        get
    //        {
    //            return instance;
    //        }
    //    }
    //}

    [TestFixture]
    public class TestOnlinerClass
    {
        private IWebDriver driver; // = SingletonWebDriverChrome.Instance;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
                // driver = SingletonWebDriverChrome.Instance;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }

        [Test,Repeat(3)]
        public void TestMethod()
        {
            bool notDisplayed = true;
            string topicName = "", result = "";
            HomePage home = new HomePage(driver);
            home.NavigateHomePage();
            Assert.AreEqual("Onliner.by", driver.Title);
            home.ClickAuth();
            LoginPage log = new LoginPage(driver);
            Authorization(log);
            var profile = home.GetProfileElement();
            Assert.IsTrue(profile.Enabled);
            home.GetListTopcis();
            while (notDisplayed)
            {
                var topic = home.GetRandomTopic();
                if (topic.Displayed)
                {
                    notDisplayed = false;
                    topicName = topic.Text;
                    TopicPage top = home.NavigateToRandomTopic(topic);
                    result = top.GetNameOfTopic();
                }
            }
            Assert.AreEqual(topicName, result);
            home.NavigateHomePage();
            home.WriteAllOpinionInCsv();
            home.Logout();
            Assert.AreEqual("Вход", driver.FindElement(By.XPath(@"//*[@id=""userbar""]/div/div[1]")).Text);
        }

        private void Authorization(LoginPage loginPage)
        {
            loginPage.EnterUsername("gj-arrow");
            loginPage.EnterPassword("FlaTron");
            loginPage.SubmitLogin();
        }
    }
}
