using System.Configuration;

namespace Onliner.Configurations
{
    public class SettingsSection : ConfigurationSection
    {
        private static readonly SettingsSection _settings = ConfigurationManager.GetSection("Settings") as SettingsSection;
        public static SettingsSection Settings {
            get { return _settings; }
        }

        [ConfigurationProperty("Browser")]
        public string Browser
        {
            get { return this["Browser"] as string; }
            set { this["Browser"] = value; }
        }

        [ConfigurationProperty("Url")]
        public string Url
        {
            get { return this["Url"] as string; }
            set { this["Url"] = value; }
        }

        [ConfigurationProperty("PathToFile")]
        public string PathToFile
        {
            get { return this["PathToFile"] as string; }
            set { this["PathToFile"] = value; }
        }

        [ConfigurationProperty("Username")]
        public string Username
        {
            get { return this["Username"] as string; }
            set { this["Username"] = value; }
        }

        [ConfigurationProperty("Password")]
        public string Password
        {
            get { return this["Password"] as string; }
            set { this["Password"] = value; }
        }

        [ConfigurationProperty("NameFile")]
        public string NameFile
        {
            get { return this["NameFile"] as string; }
            set { this["NameFile"] = value; }
        }

        [ConfigurationProperty("ImplicitWait")]
        public int ImplicitWait
        {
            get { return (int)this["ImplicitWait"]; }
            set { this["ImplicitWait"] = value; }
        }
    } 
}
