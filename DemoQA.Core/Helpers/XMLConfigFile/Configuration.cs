using System;
using System.Xml.Serialization;

namespace DemoQA.Core.Helpers.XMLConfigFile
{
    [Serializable]
    public class Configuration
    {
        [XmlElement("Environment")]
        public Environment Environment { get; set; }
    }
}
