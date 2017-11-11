using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Task2
{
    public class TopicPage
    {
        private IWebDriver driver;

        public TopicPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetNameOfTopic()
        {
            return driver.FindElement(By.XPath("//*[@id='container']/div/div[2]/div/div/div[2]/div[2]/div[2]/h1")).Text;
        }
    }
}
