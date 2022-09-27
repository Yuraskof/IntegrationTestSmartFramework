using SmartVkApi.Base;
using SmartVkApi.Constants;
using SmartVkApi.Forms.Pages;
using SmartVkApi.Models;
using SmartVkApi.Forms;
using SmartVkApi.Utilities;
using RestApiTask.Utils;

namespace SmartVkApi.Tests
{
    public class TestVkWall : BaseTest
    {
        [Test(Description = "TC-0001 Check the Vk wall functionality")]
        [TestCaseSource(nameof(PrepareToTest))]
        public void TC0001_CheckTheVkWallFunctionality(LocalizedTestDataModel model)
        {
            LoginOrRegistrationPage loginOrRegistrationPage = new LoginOrRegistrationPage();
            Assert.IsTrue(loginOrRegistrationPage.State.WaitForDisplayed(), $"{loginOrRegistrationPage.Name} should be presented");
            loginOrRegistrationPage.SelectLanguage(model);
            loginOrRegistrationPage.PerformLogin(model);
            Assert.IsTrue(loginOrRegistrationPage.passwordForm.State.WaitForDisplayed(), $"{loginOrRegistrationPage.passwordForm.Name} should be presented");
            loginOrRegistrationPage.passwordForm.PerformPassword(model);
            Logger.Info("Step 2 completed.");

            SideNavigationForm sideNavigationForm = new SideNavigationForm();
            Assert.IsTrue(sideNavigationForm.State.WaitForDisplayed(), $"{sideNavigationForm.Name} should be presented");
            if (!sideNavigationForm.GoToMyProfilePage(model))
            {
                SelectLanguageForm selectLanguageForm = new SelectLanguageForm();
                selectLanguageForm.SelectLanguage(model);
                sideNavigationForm.GoToMyProfilePage(model);
            }

            MyProfilePage myProfilePage = new MyProfilePage();
            Assert.IsTrue(myProfilePage.State.WaitForDisplayed(), $"{myProfilePage.Name} should be presented");
            Logger.Info("Step 3 completed.");

            string postMessage = StringUtils.StringGenerator(Convert.ToInt32(BaseTest.testData.LettersCount));
            WallPostModel postModel = ModelUtils.CreateWallPostModel(postMessage);
            WallPostResponseModel postResponseModel = ApiApplicationRequest.CreatePostOnTheWall(postModel);
            Assert.NotNull(postResponseModel.response.post_id, $"{sideNavigationForm.Name} should be presented");
            Logger.Info("Step 4 completed.");

        }

        public static IEnumerable<object[]> PrepareToTest()
        {
            FileReader.ClearLogFile();

            List<LocalizedTestDataModel> modelsList = ModelUtils.GetModels();

            foreach (var model in modelsList)
            {
                yield return new[] { model };
            }
        }
    }
}