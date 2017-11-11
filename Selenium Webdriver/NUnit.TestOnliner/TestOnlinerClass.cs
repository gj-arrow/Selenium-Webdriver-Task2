using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task2;
using System.Threading;

namespace NUnit.TestOnliner
{
    [TestFixture]
    public class TestOnlinerClass
    {
        private IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
        }


        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }

        [Test]
        public void TestMethod()
        {
            driver.Manage().Window.Maximize();
            HomePage home = new HomePage(driver);
            home.GoHomePage();
            home.ClickAuth();
            LoginPage log = new LoginPage(driver);
            Thread.Sleep(2000);
            log.EnterUsername("gj-arrow");
            log.EnterPassword("FlaTron");
            Thread.Sleep(4000);
            home = log.SubmitLogin();
            Thread.Sleep(2000);
            home.GetListTopcis();
            Thread.Sleep(2000);
            var topic = home.GetRandomTopic();
            var topicName = topic.Text;
            TopicPage top = home.GoToRandomTopic(topic);
            var a = top.GetNameOfTopic();
            Assert.AreEqual(topicName, a);
            home.GoHomePage();
            home.WriteAllOpinionInCSV();
            home.Logout();
        }
    }
}
