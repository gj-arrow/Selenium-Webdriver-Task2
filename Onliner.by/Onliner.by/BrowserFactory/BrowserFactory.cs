using System;
using Onliner.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Onliner.Helpers;

namespace Onliner.BrowserFactory
{
  public class BrowserFactory
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return _driver;
            }
        }

        public static void InitBrowser(string browserName)
        {
            var currentBrowser = GetCurrentBrowser();
            switch (currentBrowser)
            {
                case BrowserNameHelper.BrowserEnum.FIREFOX:
                    {
                        _driver = new FirefoxDriver();
                    }
                    break;

                case BrowserNameHelper.BrowserEnum.CHROME:
                    {
                        _driver = new ChromeDriver();
                    }
                    break;

                default:
                {
                    throw new Exception("Invalid browser name.");
                }
            }
        }

        private static BrowserNameHelper.BrowserEnum GetCurrentBrowser()
        {
            return (BrowserNameHelper.BrowserEnum)Enum.Parse(typeof(BrowserNameHelper.BrowserEnum),Config.Browser.ToUpper());
        }

        public static void CloseDriver()
        {
            _driver.Close();
            _driver.Quit();
        }
    }
}
