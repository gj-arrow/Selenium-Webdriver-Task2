using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Task2
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private IWebElement buttonAuth;
        private IWebElement profileMenu;
        private IWebElement buttonLogOut;
        private ReadOnlyCollection<IWebElement> listTopics;
        private Random random = new Random();


        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public HomePage NavigateHomePage()
        {
            driver.Navigate().GoToUrl("https://www.onliner.by/");
            return this;
        }

        public HomePage ClickAuth()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            buttonAuth = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='userbar']/div/div[1]")));
            buttonAuth.Click();
            return this;
        }

        public HomePage GetListTopcis()
        {
            listTopics = driver.FindElements(By.XPath("//*[@id='container']/div/div[2]/div/div/div[1]/div/div[1]/ul/li"));
            return this;
        }

        public IWebElement GetRandomTopic()
        {
            var numberTopic = random.Next(0, listTopics.Count);
            var randomTopic = listTopics.ToArray()[numberTopic].FindElement(By.TagName("a"));
            return randomTopic;
        }


        public IWebElement GetProfileElement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement profile = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//*[@id=""userbar""]/div[1]/div[1]/a[contains(@href,""profile"")]")));
            return profile;
        }

        public TopicPage NavigateToRandomTopic(IWebElement topic)
        {  
            driver.Navigate().GoToUrl(topic.GetAttribute("href"));
            return new TopicPage(driver);
        }

        public void WriteAllOpinionInCsv()
        {
            var pageSource = driver.PageSource;
            var allOpinion = new List<string>();
            foreach (Match match in Regex.Matches(pageSource, @"b-opinions-main-2__text"">(.+)(?=[<])", RegexOptions.IgnoreCase))
            {
                allOpinion.Add(match.Groups[1].Value);
            }
            using (StreamWriter writer = File.CreateText("Opinions.csv"))
            {
                foreach (var item in allOpinion)
                {
                    writer.WriteLine(item);
                }
            }
        }

        public LoginPage Logout()
        {
            Actions actions = new Actions(driver);
            profileMenu = driver.FindElement(By.XPath(@"//*[@id=""userbar""]/div[1]/div[1]/a"));
            actions.MoveToElement(profileMenu).Click().Build().Perform();
               // profileMenu.Click();
            buttonLogOut = driver.FindElement(By.XPath(@"//*[@id=""userbar""]/div[1]/div[1]/div/div[1]/div[1]/div[2]/div/a"));
            //    actions.MoveToElement(buttonLogOut).Click().Build().Perform();
            buttonLogOut.Click();
            return new LoginPage(driver);
        }
    }
}