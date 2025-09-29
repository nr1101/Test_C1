using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AQATestProject.Pages
{
    public class CasesPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By _casesMenuButton = By.CssSelector("a[title='Дела']");
        private readonly By _createFolderButton = By.CssSelector("a.b-sidebar-menu-link--project_folder--add");
        private readonly By _folderNameInput = By.CssSelector("input[name='folderNew']"); 
        private readonly By _foldersList = By.CssSelector("div.b-sidebar-menu-text--sub-folder_name"); 

        private readonly By _createFloatingButton = By.CssSelector("common-floating-button div.b-floating_button-el");
        private readonly By _createCaseButton = By.CssSelector("div.b-floating_button-icon_list-item--projects_add_case > div.b-floating_button-icon_list-item-content");

        private readonly By _caseNameInput = By.Name("Name");
        private readonly By _caseFolderInput = By.CssSelector("input[name='ProjectFolder']");
        private readonly By _caseTypeInput = By.CssSelector("input[name='ProjectType']");

        private readonly By _dropdown = By.CssSelector(".b-autocomplete-dropdown"); 
        private readonly By _dropdownOptions = By.CssSelector(".b-autocomplete-item_name span");

        private readonly By _submitButton = By.CssSelector("button.b-button--submit");
        private readonly By _caseTitleHeader = By.CssSelector("div.title.b-object_card_header_title");

        /// <summary>
        /// Навигация
        /// </summary>
        public void GoToCases()
        {
            WaitForElement(_casesMenuButton).Click();
        }

        public void WaitForCasesMenuButton()
        {
            WaitForElement(_casesMenuButton);
        }

        public void WaitForFloatingButton()
        {
            WaitForElement(_createFloatingButton);
        }

        /// <summary>
        /// Работа с папками
        /// </summary>
        public void CreateFolder(string folderName)
        {
            WaitForClickable(_createFolderButton).Click();
            var input = WaitForElement(_folderNameInput);
            input.Click();
            input.SendKeys(folderName + Keys.Enter);
            Wait.Until(d => FolderExists(folderName));
        }

        public bool FolderExists(string folderName)
        {
            return Driver.FindElements(_foldersList)
                .Any(f => f.Text.Trim() == folderName);
        }

        /// <summary>
        /// Работа с делами
        /// </summary>
        public void ClickFloatingButton()
        {
            new Actions(Driver).MoveToElement(WaitForClickable(_createFloatingButton)).Click().Perform();
        }

        public void ClickCreateCase()
        {
            new Actions(Driver).MoveToElement(WaitForClickable(_createCaseButton)).Click().Perform();
        }

        public void EnterCaseName(string caseName)
        {
            new Actions(Driver).MoveToElement(WaitForClickable(_caseNameInput))
                .Click()
                .SendKeys(caseName)
                .Perform();
        }

        public void SelectFolder(string folderName)
        {
            SelectDropdownOption(_caseFolderInput, folderName);
        }

        public void SelectType(string typeName)
        {
            SelectDropdownOption(_caseTypeInput, typeName);
        }

        public void SubmitCase()
        {
            WaitForClickable(_submitButton).Click();
        }

        public void SwitchToNewTab()
        {
            var originalTab = Driver.CurrentWindowHandle;
            Wait.Until(driver => driver.WindowHandles.Count > 1);

            foreach (var tab in Driver.WindowHandles)
            {
                if (tab != originalTab)
                {
                    Driver.SwitchTo().Window(tab);
                    break;
                }
            }
            WaitForElement(_caseTitleHeader);
        }

        private void SelectDropdownOption(By inputLocator, string value)
        {
            WaitForClickable(inputLocator).Click();
            var dropdown = WaitForElement(_dropdown);
            var option = dropdown.FindElements(_dropdownOptions)
                .FirstOrDefault(e => e.Text.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (option == null) throw new Exception($"Item '{value}' was not found");
            option.Click();
        }

        public void CreateCase(string caseName, string folderName, string typeName)
        {
            ClickFloatingButton();
            ClickCreateCase();
            EnterCaseName(caseName);
            SelectFolder(folderName);
            SelectType(typeName);
            SubmitCase();
            SwitchToNewTab();
        }

        public bool IsCaseOpened(string caseName)
        {
            var caseHeader = WaitForElement(_caseTitleHeader);
            return caseHeader.Text.Contains(caseName);
        }
    }
}
