using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AQATestProject.Pages
{
    public abstract class BasePage(IWebDriver driver)
    {
        protected readonly IWebDriver Driver = driver;
        protected readonly WebDriverWait Wait = new(driver, TimeSpan.FromSeconds(30));

        public IWebElement WaitForElement(By locator)
        {
            return Wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.Displayed ? element : null;
            });
        }

        protected IWebElement WaitForClickable(By locator)
        {
            return Wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return (element.Displayed && element.Enabled) ? element : null;
            });
        }

    }
}
