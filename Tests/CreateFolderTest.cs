using NUnit.Framework;
using AQATestProject.Pages;
using AQATestProject.Utils;

namespace AQATestProject.Tests
{
    [TestFixture]
    public class CreateFolderTest : BaseTest
    {
        /// <summary>
        /// Тест создания папки в разделе "Дела"
        /// </summary>
        [Test]
        public void CreateFolder_ShouldAppearInList()
        {
            var loginPage = new LoginPage(Driver);

            loginPage.Open(Config.Url);
            loginPage.Login(Config.Username, Config.Password);

            var casesPage = new CasesPage(Driver);

            casesPage.GoToCases();

            string folderName = "Тест создания_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH:mm");
            casesPage.CreateFolder(folderName);

            Assert.That(casesPage.FolderExists(folderName), Is.True, $"Folder '{folderName}' was not found");
        }
    }
}
