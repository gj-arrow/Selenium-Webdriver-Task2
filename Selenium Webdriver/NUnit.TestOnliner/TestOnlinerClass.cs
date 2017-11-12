using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task2;
using System.Threading;
using System;
using OpenQA.Selenium.Support.UI;

namespace NUnit.TestOnliner
{
    public class SingletonWebDriverChrome
    {
        static readonly IWebDriver instance = new ChromeDriver();
        private IWebDriver driver;

        private SingletonWebDriverChrome()
        {
        }
        public static IWebDriver Instance
        {
            get
            {
                return instance;
            }
        }
    }

    [TestFixture]
    public class TestOnlinerClass
    {
        private IWebDriver driver;// = SingletonWebDriverChrome.Instance;

        [SetUp]
        public void Initialize()
        {
             driver = new ChromeDriver();
           // driver = SingletonWebDriverChrome.Instance;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }

        [Test]
        public void TestMethod()
        {
            HomePage home = new HomePage(driver);
            home.GoHomePage();
            Assert.AreEqual("Onliner.by", driver.Title);
            home.ClickAuth();
            LoginPage log = new LoginPage(driver);
            log.EnterUsername("gj-arrow");
            log.EnterPassword("FlaTron");
            Thread.Sleep(4000);
            home = log.SubmitLogin();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Func<IWebDriver, IWebElement> waitForElement = (web) =>
            {
                IWebElement profileElement = web.FindElement(By.XPath(@"//*[@id=""userbar""]/div[1]/div[1]/a[contains(@href,""profile"")]"));
                return profileElement;
            };
            IWebElement profile = wait.Until(waitForElement);
            Assert.IsTrue(profile.Enabled);
            //   Thread.Sleep(4000);
            // Assert.IsTrue(driver.FindElement(By.XPath(@"//*[@id=""userbar""]/div[1]/div[1]/a[contains(@href,""profile"")]")).Enabled);
            home.GetListTopcis();
            var topic = home.GetRandomTopic();
            var topicName = topic.Text;
            TopicPage top = home.GoToRandomTopic(topic);
            var a = top.GetNameOfTopic();
            Assert.AreEqual(topicName, a);
            home.GoHomePage();
            home.WriteAllOpinionInCSV();
            home.Logout();
            Assert.AreEqual("Вход", driver.FindElement(By.XPath(@"//*[@id=""userbar""]/div/div[1]")).Text);
        }

        [Test, Repeat(10)]
        public void TestMethod1()
        {
            bool notDisplayed = true;
            string topicName="",result ="";
            HomePage home = new HomePage(driver);
            home.GoHomePage();
            home.GetListTopcis();
            while (notDisplayed)
            {
                var topic = home.GetRandomTopic();
                if (topic.Displayed)
                {
                    notDisplayed = false;
                    topicName = topic.Text;
                    TopicPage top = home.GoToRandomTopic(topic);
                    result = top.GetNameOfTopic();
                }
            }
            Assert.AreEqual(topicName, result);
        }


        //[Test,Repeat(15)]
        //public void TestMethod1()
        //{
        //    HomePage home = new HomePage(driver);
        //    home.GoHomePage();
        //    home.GetListTopcis();
        //    var topic = home.GetRandomTopic2();
        //  //  var topicName = topic.Text;
        //    //if (string.IsNullOrEmpty(topic.Text))
        //    //{
        //    //    int i = 12;
        //    //}
        //    TopicPage top = home.GoToRandomTopic2(topic);
        //    var a = top.GetNameOfTopic();
        //    if (string.IsNullOrEmpty(topic))
        //    {
        //        int i = 12;
        //    }
        //    Assert.AreEqual(topic, a);
        //}
    }
}
