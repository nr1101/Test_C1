using OpenQA.Selenium;

namespace AQATestProject.Pages
{
    public class LoginPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By _usernameInput = By.CssSelector("input[name='login']");
        private readonly By _passwordInput = By.CssSelector("input[name='password']"); 
        private readonly By _loginButton = By.CssSelector("button[type='submit']");

        public void Open(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Login(string username, string password)
        {
            WaitForElement(_usernameInput).SendKeys(username);
            WaitForElement(_passwordInput).SendKeys(password);
            WaitForClickable(_loginButton).Click();
        }
    }
}
