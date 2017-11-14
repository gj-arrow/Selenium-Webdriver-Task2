using System;
using System.Configuration;


namespace Onliner.Configurations
{
    public class CustomSettingsSection : ConfigurationSection
    {
        [ConfigurationProperty("Browser")]
        public CustomSettingsElement Browser
        {
            get { return this["Browser"] as CustomSettingsElement; }
            set { this["Browser"] = value; }
        }

        [ConfigurationProperty("Url")]
        public CustomSettingsElement Url
        {
            get { return this["Url"] as CustomSettingsElement; }
            set { this["Url"] = value; }
        }

        //[ConfigurationProperty("PathToFile")]
        //public CustomSettingsElement PathToFile
        //{
        //    get { return this["PathToFile"] as CustomSettingsElement; }
        //    set { this["PathToFile"] = value; }
        //}


        public CustomSettings TakeSettingsFromConfig()
        {
            return new CustomSettings()
            {
                Browser = this.Browser.InnerText,
                Url = this.Url.InnerText,
               // PathToFile = this.PathToFile.InnerText
            };
        }
    }
}
