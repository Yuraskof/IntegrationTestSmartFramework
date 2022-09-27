using SmartVkApi.Base;
using SmartVkApi.Constants;
using SmartVkApi.Models;

namespace SmartVkApi.Utilities
{
    public class ModelUtils
    {
        public static List<LocalizedTestDataModel> GetModels()
        {
            BaseTest.Logger.Info("Start creating model");

            var json = File.ReadAllText(ProjectConstants.PathToLocalizedTestData);
            var jsonObj = JsonUtils.ParseToJsonObject(json);
            var testData = jsonObj["Localizations"].ToString();

            var modelsList = JsonUtils.ReadJsonData<List<LocalizedTestDataModel>>(testData);

            return modelsList;
        }

        public static WallPostModel CreateWallPostModel(object content)
        {
            WallPostModel postModel = new WallPostModel();
            postModel.message = content.ToString();
            postModel.v = BaseTest.testData.ApiVersion;
            postModel.owner_id = BaseTest.testData.UserId;
            postModel.access_token = BaseTest.testData.Token;
            return postModel;
        }
    }
}
