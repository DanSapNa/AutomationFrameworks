using NUnit.Framework;

namespace DemoQA.Tests.Tests
{
    [TestFixture]
    public class Tests : TestFunctional
    {
        [Test]
        public void Test1()
        {
            page.Logo.Click();
        }

        [Test]
        public void Test2()
        {

        }
    }
}
