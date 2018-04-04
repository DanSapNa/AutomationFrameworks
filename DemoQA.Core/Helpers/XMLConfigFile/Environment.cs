using System;

namespace DemoQA.Core.Helpers.XMLConfigFile
{
    [Serializable]
    public class Environment
    {
        public string url;
        public string browser;
        public string username;
        public string password;
        public string mode;
        public string report;
    }
}
