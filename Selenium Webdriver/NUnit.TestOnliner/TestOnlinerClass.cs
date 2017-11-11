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
            home = log.SubmitLogin();
            Thread.Sleep(2000);
            home.Logout();
            Thread.Sleep(2000);
        }
    }
}
