using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Onliner.BrowserFactory
{
  public class BrowserFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return _driver;
            }
            private set
            {
                _driver = value;
            }
        }

        public static void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "firefox":
                    {
                        _driver = new FirefoxDriver();
                    Drivers.Add("firefox", Driver);
                    }
                    break;

                case "chrome":
                    {
                        _driver = new ChromeDriver();
                        Drivers.Add("chrome", Driver);
                    }
                    break;
            }
        }

        public static void NavigateToUrl(string url)
        {
            Driver.Url = url;
        }

        public static void CloseDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
