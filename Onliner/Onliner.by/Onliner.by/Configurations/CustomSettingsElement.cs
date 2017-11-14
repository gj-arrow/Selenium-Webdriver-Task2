using System;
using System.Configuration;
using System.Xml;

namespace Onliner.Configurations
{
    public class CustomSettingsElement : ConfigurationElement
    {
        public string InnerText { get; private set; }

        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            InnerText = reader.ReadElementContentAsString();
        }
    }
}
