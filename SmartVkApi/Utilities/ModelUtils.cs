using SmartVkApi.Base;
using SmartVkApi.Constants;
using SmartVkApi.Models;
using SmartVkApi.Models.RequestModels;
using SmartVkApi.Models.ResponseModels;

namespace SmartVkApi.Utilities
{
    public static class ModelUtils
    {
        public static UploadImageResponseModel uploadImageResponse = new UploadImageResponseModel();

        public static List<LocalizedTestDataModel> GetModels()
        {
            LoggerUtils.LogStep(nameof(GetModels) + " \"Start creating localized test data model\"");
            var json = File.ReadAllText(FileConstants.PathToLocalizedTestData);
            var jsonObj = JsonUtils.ParseToJsonObject(json);
            var testData = jsonObj["Localizations"].ToString();
            var modelsList = JsonUtils.ReadJsonData<List<LocalizedTestDataModel>>(testData);
            return modelsList;
        }

        public static WallPostModel CreateWallPostModel(object content, string attachments = null, string postId = null)
        {
            LoggerUtils.LogStep(nameof(CreateWallPostModel) + " \"Start creating wall post model\"");
            WallPostModel postModel = new WallPostModel();
            postModel.message = content.ToString();
            postModel.v = BaseTest.testData.ApiVersion;
            postModel.owner_id = BaseTest.testData.UserId;
            postModel.access_token = BaseTest.testData.Token;
            postModel.attachments = attachments;
            postModel.post_id = postId;
            return postModel;
        }

        public static GetUploadUrlModel CreateGetUploadUrlModel(object content)
        {
            LoggerUtils.LogStep(nameof(CreateGetUploadUrlModel) + " \"Start creating get upload url model\"");
            GetUploadUrlModel model = new GetUploadUrlModel();
            model.group_id = content.ToString();
            model.v = BaseTest.testData.ApiVersion;
            model.access_token = BaseTest.testData.Token;
            return model;
        }

        public static SaveWallPhotoModel CreateSaveWallPhotoModel()
        {
            LoggerUtils.LogStep(nameof(CreateSaveWallPhotoModel) + " \"Start creating wall post model\"");
            SaveWallPhotoModel model = new SaveWallPhotoModel();
            model.user_id = BaseTest.testData.UserId;
            model.v = BaseTest.testData.ApiVersion;
            model.photo = ModelUtils.uploadImageResponse.photo;
            model.server = Convert.ToString(ModelUtils.uploadImageResponse.server);
            model.hash = ModelUtils.uploadImageResponse.hash;
            model.access_token = BaseTest.testData.Token;
            return model;
        }

        public static WallCommentModel CreateWallCommentModel(object content, string attachments = null, string postId = null)
        {
            LoggerUtils.LogStep(nameof(CreateWallCommentModel) + " \"Start creating wall comment model\"");
            WallCommentModel model = new WallCommentModel();
            model.message = content.ToString();
            model.v = BaseTest.testData.ApiVersion;
            model.owner_id = BaseTest.testData.UserId;
            model.access_token = BaseTest.testData.Token;
            model.attachments = attachments;
            model.post_id = postId;
            return model;
        }

        public static GetLikesRequestModel CreateGetLikesRequestModel(object content)
        {
            LoggerUtils.LogStep(nameof(CreateGetLikesRequestModel) + " \"Start creating get likes request model\"");
            GetLikesRequestModel model = new GetLikesRequestModel();
            model.v = BaseTest.testData.ApiVersion;
            model.owner_id = BaseTest.testData.UserId;
            model.access_token = BaseTest.testData.Token;
            model.post_id = content.ToString();
            return model;
        }

        public static bool FindLikeFromUser(GetLikesResponseModel getLikesResponseModel, WallPostModel postModel)
        {
            LoggerUtils.LogStep(nameof(FindLikeFromUser) + " \"Start searching like from desired user\"");

            foreach (var user in getLikesResponseModel.response.users)
            {
                if (user.uid == postModel.owner_id)
                {
                    return true;
                }
            }
            return false;
        }

        public static DeletePostFromWallModel CreateDeletePostFromWallModel(object content)
        {
            LoggerUtils.LogStep(nameof(CreateDeletePostFromWallModel) + " \"Start creating delete post from wall request model\"");
            DeletePostFromWallModel model = new DeletePostFromWallModel();
            model.v = BaseTest.testData.ApiVersion;
            model.owner_id = BaseTest.testData.UserId;
            model.access_token = BaseTest.testData.Token;
            model.post_id = content.ToString();
            return model;
        }
    }
}
