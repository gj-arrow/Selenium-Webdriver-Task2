using System;
using OpenQA.Selenium;

namespace Onliner.PageObjects
{
    public class TopicPage
    {
        private readonly IWebDriver _driver;

        public TopicPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetNameOfTopic()
        {
            return _driver.FindElement(By.CssSelector("div[class='schema-header']> h1[class='schema-header__title']")).Text;
        }
    }
}
