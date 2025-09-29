using NUnit.Framework;
using OpenQA.Selenium;
using AQATestProject.Drivers;

namespace AQATestProject.Tests
{
    
    public abstract class BaseTest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            Driver = DriverFactory.CreateDriver();
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
               Driver.Quit();
               Driver.Dispose();
            }
        }
    }
}
