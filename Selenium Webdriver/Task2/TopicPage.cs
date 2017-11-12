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
            return driver.FindElement(By.CssSelector("#container > div > div.l-gradient-wrapper > div > div > div.catalog-content.js-scrolling-area > div.schema-grid__wrapper > div.schema-header > h1")).Text;
        }
    }
}
