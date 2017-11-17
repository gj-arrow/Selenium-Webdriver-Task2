using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Onliner.Services
{
   public static class WaitService
    {
        public static bool WaitTillElementisDisplayed(IWebDriver driver, By by, int timeoutInSeconds)
        {
            var elementDisplayed = false;
            for (var i = 0; i < timeoutInSeconds; i++)
            {
                if (timeoutInSeconds > 0)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.Until(drv => drv.FindElement(by));
                }
                elementDisplayed = driver.FindElement(by).Displayed;
            }
            return elementDisplayed;
        }
    }
}
