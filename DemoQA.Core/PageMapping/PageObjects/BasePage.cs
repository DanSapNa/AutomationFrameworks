using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DemoQA.Core.PageMapping.PageObjects
{
    public class BasePage
    {
        private IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@class='navbar-header']//img")]
        public IWebElement Logo { get; set; }
    }
}
