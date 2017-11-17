using OpenQA.Selenium;

namespace Onliner.PageObjects
{
    public class TopicPage
    {
        private readonly IWebDriver _driver;
        private readonly By _textTopicLocator = By.CssSelector("div[class*='schema-header']> h1[class*='schema-header__title']");

        public TopicPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetNameOfTopic()
        {
            return _driver.FindElement(_textTopicLocator).Text;
        }
    }
}
