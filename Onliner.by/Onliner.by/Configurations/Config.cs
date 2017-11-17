using System;
using System.Configuration;

namespace Onliner.Configurations
{
    public static class Config
    {
        public static string Browser {
            get { return ConfigurationManager.AppSettings["Browser"]; }
        }

        public static string Url
        {
            get { return ConfigurationManager.AppSettings["Url"]; }
        }

        public static int ImplicitWait
        {
            get { return Int32.Parse(ConfigurationManager.AppSettings["ImplicitWait"]); }
        }

        public static int ExplicitWait
        {
            get { return Int32.Parse(ConfigurationManager.AppSettings["ExplicitWait"]); }
        }

        public static string PathToFile
        {
            get { return ConfigurationManager.AppSettings["PathToFile"]; }
        }

        public static string Username
        {
            get { return ConfigurationManager.AppSettings["Username"]; }
        }

        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        public static string NameFile
        {
            get { return ConfigurationManager.AppSettings["NameFile"]; }
        }
    }
}
