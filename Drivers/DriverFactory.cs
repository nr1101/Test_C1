using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AQATestProject.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");          
            options.AddArgument("--disable-notifications");

            return new ChromeDriver(options); 
        }
    }
}
