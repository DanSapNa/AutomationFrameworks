using DemoQA.Core.Helpers.XMLConfigFile;
using DemoQA.Core.PageMapping.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DemoQA.Tests
{
    public class TestFunctional
    {
        private static string pathToBinDebugFolder = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
        //private static string pathToReportsFolder = Path.Combine(Directory.GetParent(pathToBinDebugFolder).Parent.FullName, "Reports");
        private const string CONFIG_FILE_NAME = "Configuration.xml";
        private const string PATH_TO_FIREFOX = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
        private const string CHROME_MODE = "headless";

        private Configuration configFile;
        private IWebDriver driver;
        private Deserialization deserialize = new Deserialization();
        public BasePage page;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            try
            {
                string pathToConfig = Path.Combine(Directory.GetParent(pathToBinDebugFolder).Parent.FullName, CONFIG_FILE_NAME);
                configFile = deserialize.Deserialize<Configuration>(pathToConfig);

                KillDriverProcess();
            }
            catch (Exception ex)
            { 
                //("[TestFixtureSetUp] method failed on exception. This can fail the complete test set. Exception: " + ex.Message + "Source: " + ex.Source + ". Target Site: " + ex.TargetSite, false, true); 
            }
        }

        [SetUp]
        public void SetUp()
        {
            switch (configFile.Environment.browser)
            {
                case "CH":
                    if (configFile.Environment.mode.ToLower().Equals(CHROME_MODE))
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments($"--{CHROME_MODE}");
                        driver = new ChromeDriver(chromeOptions);
                    }
                    else
                        driver = new ChromeDriver();
                    break;
                case "FF":
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(pathToBinDebugFolder);
                    service.FirefoxBinaryPath = PATH_TO_FIREFOX;
                    driver = new FirefoxDriver(service);
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
            }

            driver.Navigate().GoToUrl(configFile.Environment.url);
            driver.Manage().Window.Maximize();

            page = new BasePage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        public void KillDriverProcess()
        {
            switch (configFile.Environment.browser)
            {
                case "CH":
                    Process.GetProcesses().Where(pr => pr.ProcessName.Equals("chromedriver")).ToList().ForEach(x => { x.Kill(); x.WaitForExit(); x.Dispose(); });
                    break;
                case "FF":
                    Process.GetProcesses().Where(pr => pr.ProcessName.Equals("geckodriver")).ToList().ForEach(x => { x.Kill(); x.WaitForExit(); x.Dispose(); });
                    break;
                case "IE":
                    Process.GetProcesses().Where(pr => pr.ProcessName.Equals("IEDriverServer")).ToList().ForEach(x => { x.Kill(); x.WaitForExit(); x.Dispose(); });
                    break;
            }
        }
    }
}
