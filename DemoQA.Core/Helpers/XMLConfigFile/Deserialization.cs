using System.IO;
using System.Xml.Serialization;

namespace DemoQA.Core.Helpers.XMLConfigFile
{
    public class Deserialization
    {
        private XmlSerializer formatter;
        private static object xmlObject;

        public T Deserialize<T>(string xmlPath)
        {
            formatter = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(xmlPath, FileMode.OpenOrCreate))
            {
                xmlObject = xmlObject ?? formatter.Deserialize(fs);
                return (T)xmlObject;
            }
        }

        public object XMLObject
        {
            get
            {
                return xmlObject;
            }
        }
    }
}
