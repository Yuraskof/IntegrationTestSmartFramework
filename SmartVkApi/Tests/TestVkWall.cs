using SmartVkApi.Base;
using SmartVkApi.Forms.Pages;
using SmartVkApi.Models;
using SmartVkApi.Forms;
using SmartVkApi.Utilities;
using RestApiTask.Utils;
using SmartVkApi.Models.RequestModels;
using SmartVkApi.Models.ResponseModels;

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

            WallPostModel postModel = ModelUtils.CreateWallPostModel(StringUtils.StringGenerator(Convert.ToInt32(BaseTest.testData.LettersCount)));
            WallPostResponseModel postResponseModel = ApiApplicationRequest.CreatePostOnTheWall(postModel);
            Assert.NotNull(postResponseModel, "Model should be exist");
            Assert.NotNull(postResponseModel.response.post_id, $"{postResponseModel.response.post_id} should be presented");
            Logger.Info("Step 4 completed.");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(myProfilePage.GetPostText(postModel) == postModel.message, "Messages should be equal");
                Assert.IsTrue(myProfilePage.GetAuthorLink().Contains(testData.UserId), "Wrong author");
            });
            Logger.Info("Step 5 completed.");

            string editedMessage = StringUtils.StringGenerator(Convert.ToInt32(testData.LettersCount));
            string photoId = ApiApplicationRequest.GetPhotoId();
            Assert.NotNull(photoId, "Photo id should be exist");
            WallPostModel editedPostModel = ModelUtils.CreateWallPostModel(editedMessage, photoId, postResponseModel.response.post_id);
            WallPostResponseModel editedPostResponseModel = ApiApplicationRequest.EditPostOnTheWall(editedPostModel);
            Assert.NotNull(editedPostResponseModel, "Model should be exist");
            Logger.Info("Step 6 completed.");

            Assert.IsTrue(myProfilePage.GetPostText(editedPostModel) != postModel.message, "Messages should be different");
            myProfilePage.OpenFullSizeImage(photoId);
            Assert.IsTrue(myProfilePage.fullSizeImageForm.State.WaitForDisplayed(), $"{myProfilePage.fullSizeImageForm.Name} should be presented");
            float difference = myProfilePage.fullSizeImageForm.CompareImages();
            Assert.IsTrue(difference < 0.01, "Images are not equal");
            Logger.Info("Step 7 completed.");

            myProfilePage.fullSizeImageForm.CloseForm();
            string commentMessage = StringUtils.StringGenerator(Convert.ToInt32(testData.LettersCount));
            WallCommentModel wallCommentModel = ModelUtils.CreateWallCommentModel(commentMessage, postId: editedPostResponseModel.response.post_id);
            WallCommentResponseModel wallCommentResponseModel = ApiApplicationRequest.AddCommentOnTheWall(wallCommentModel);
            Assert.NotNull(wallCommentResponseModel, "Model should be exist");
            Logger.Info("Step 8 completed.");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(myProfilePage.GetCommentText(editedPostModel, wallCommentModel) == wallCommentModel.message, "Messages should be equal");
                Assert.IsTrue(myProfilePage.GetCommentAuthorLink(editedPostModel, wallCommentModel).Contains(testData.UserId), "Wrong author");
            });
            Logger.Info("Step 9 completed.");

            myProfilePage.AddLike(editedPostModel);
            Logger.Info("Step 10 completed.");

            GetLikesRequestModel getLikesRequestModel = ModelUtils.CreateGetLikesRequestModel(editedPostResponseModel.response.post_id);
            GetLikesResponseModel getLikesResponseModel = ApiApplicationRequest.GetLikesFromTheWallPost(getLikesRequestModel);
            Assert.NotNull(getLikesResponseModel, "Model should be exist");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(getLikesResponseModel.response.count > 0, "Post should contains like");
                Assert.IsTrue(ModelUtils.FindLikeFromUser(getLikesResponseModel, editedPostModel), "Post doesn't contain like from the desired user");
            });
            Logger.Info("Step 11 completed.");

            DeletePostFromWallModel deletePostModel = ModelUtils.CreateDeletePostFromWallModel(editedPostResponseModel.response.post_id);
            DeletePostFromWallResponseModel deletePostFromWallResponseModel = ApiApplicationRequest.DeletePost(deletePostModel);
            Assert.NotNull(deletePostFromWallResponseModel, "Model should be exist");
            Assert.IsTrue(deletePostFromWallResponseModel.response == 1, "Response doesn't equal 1");
            Logger.Info("Step 12 completed.");

            Assert.IsTrue(myProfilePage.CheckThePostNotExist(editedPostModel), "Post not deleted");
            Logger.Info("Step 13 completed. Test case successfully finished.");
        }

        public static IEnumerable<object[]> PrepareToTest()
        {
            FileReader.ClearLogFile();

            List<LocalizedTestDataModel> modelsList = ModelUtils.GetModels();

            LoggerUtils.LogStep(nameof(PrepareToTest) + " \"Get localized models\"");

            foreach (var model in modelsList)
            {
                yield return new[] { model };
            }
        }
    }
}