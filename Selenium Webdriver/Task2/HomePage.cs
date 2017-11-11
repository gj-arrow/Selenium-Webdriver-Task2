using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;

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

        public void GoHomePage()
        {
            driver.Navigate().GoToUrl("https://www.onliner.by/");
        }

        public void ClickAuth()
        {
            buttonAuth = driver.FindElement(By.XPath("//*[@id='userbar']/div/div[1]"));
            if (buttonAuth.Enabled)
            {
                buttonAuth.Click();
            }
        }

        public void GetListTopcis()
        {
            listTopics = driver.FindElements(By.XPath("//*[@id='container']/div/div[2]/div/div/div[1]/div/div[1]/ul/li"));
        }

        public IWebElement GetRandomTopic()
        {
            var numberTopic = random.Next(0, listTopics.Count);
            var selectedTopic = listTopics.ToArray()[numberTopic].FindElement(By.TagName("a"));
            return selectedTopic;
        }

        public TopicPage GoToRandomTopic(IWebElement topic)
        {  
            driver.Navigate().GoToUrl(topic.GetAttribute("href"));
            return new TopicPage(driver);
        }

        public void WriteAllOpinionInCSV()
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

        public void Logout()
        {
            profileMenu = driver.FindElement(By.XPath("//*[@id='userbar']/div[1]/div[1]/a"));
            if (profileMenu.Enabled)
            {
                profileMenu.Click();
            }
            buttonLogOut = driver.FindElement(By.XPath("//*[@id='userbar']/div[1]/div[1]/div/div[1]/div[1]/div[2]/div/a"));
            if (buttonLogOut.Enabled)
            {
                buttonLogOut.Click();
            }
        }
    }
}