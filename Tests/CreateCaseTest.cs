using NUnit.Framework;
using AQATestProject.Pages;
using AQATestProject.Utils;

namespace AQATestProject.Tests
{
    [TestFixture]
    public class CreateCaseTest : BaseTest
    {
        /// <summary>
        /// Тест создания дела в разеле "Дела"
        /// </summary>
        [Test]
        public void CreateCase_ShouldSucceed()
        {
            var loginPage = new LoginPage(Driver);

            loginPage.Open(Config.Url);
            loginPage.Login(Config.Username, Config.Password);

            var casesPage = new CasesPage(Driver);

            casesPage.WaitForCasesMenuButton();
            casesPage.GoToCases();
            casesPage.WaitForFloatingButton();

            string caseName = "Дело_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH:mm");
            string folderName = "папка_1";
            string typeName = "! ! 123";

            casesPage.CreateCase(caseName, folderName, typeName);

            Assert.That(casesPage.IsCaseOpened(caseName), Is.True, $"Case '{caseName}' was not opened after creation");
        }
    }
}
