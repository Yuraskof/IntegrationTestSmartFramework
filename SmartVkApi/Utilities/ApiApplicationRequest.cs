using SmartVkApi.Base;
using SmartVkApi.Constants;
using SmartVkApi.Models.RequestModels;
using SmartVkApi.Models.ResponseModels;
using System.Text;

namespace SmartVkApi.Utilities
{
    public class ApiApplicationRequest
    {
        public static Dictionary<string, string> apiMethods = FileReader.GetApiMethods();

        public static HttpResponseMessage response;

        private static string host = BaseTest.testData.Host;


        public static WallPostResponseModel CreatePostOnTheWall(WallPostModel model)
        {
            BaseTest.Logger.Info("Send post");
            string request = host + apiMethods["postOnTheWall"] + "?"+ "owner_id=" + model.owner_id + "&" +
                             "message="+model.message + "&" + "access_token=" + model.access_token + "&" + "v=" +
                             model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);

            var httpContent = new StringContent(stringContent, Encoding.UTF8, ProjectConstants.MediaType);

            response = VkApi.PostRequest(request, httpContent);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<WallPostResponseModel>(contentString);
        }

        public static WallPostResponseModel EditPostOnTheWall(WallPostModel model)
        {
            BaseTest.Logger.Info("Send edited post");
            string request = host + apiMethods["editPostOnTheWall"] + "?" + "owner_id=" + model.owner_id + "&" +
                             "post_id="+ model.post_id +"&"+ "message=" + model.message + "&" + "attachments=" + model.attachments + 
                             "&" + "access_token=" + model.access_token + "&" + "v=" + model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);

            var httpContent = new StringContent(stringContent, Encoding.UTF8, ProjectConstants.MediaType);

            response = VkApi.PostRequest(request, httpContent);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<WallPostResponseModel>(contentString);
        }

        public static GetUploadUrlResponseModel GetUploadUrl(GetUploadUrlModel model)
        {
            BaseTest.Logger.Info("Send \"Get Upload Url\" request");
            string request = host + apiMethods["getUploadUrl"] + "?" + "group_id=" + model.group_id + "&" +
                             "access_token=" + model.access_token + "&" + "v=" + model.v;

            response = VkApi.GetRequest(request);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<GetUploadUrlResponseModel>(contentString);
        }

        public static async void UploadImage(GetUploadUrlResponseModel responseModel)
        {
            BaseTest.Logger.Info("Send \"Upload image\" request");
            
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(FileReader.ReadImage(ProjectConstants.PathToWallPostImage), "photo", "postImage.png");

            response = VkApi.PostRequest(responseModel.response.upload_url, multipartContent);

            var httpContent =  await response.Content.ReadAsByteArrayAsync();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            string responseString = Encoding.GetEncoding(1251).GetString(httpContent, 0, httpContent.Length);

            ModelUtils.uploadImageResponse = JsonUtils.ReadJsonData<UploadImageResponseModel>(responseString);
        }

        public static SaveWallPhotoResponseModel SaveWallPhoto(SaveWallPhotoModel model)
        {
            BaseTest.Logger.Info("Send \"Save photo\" request");
            
            string request = host + apiMethods["saveWallPhoto"] + "?" + "group_id=" + model.user_id + "&" +
                             "photo=" + model.photo + "&" + "server=" + model.server + "&" + "hash="+model.hash + "&" + 
                             "access_token=" + model.access_token + "&" + "v=" + model.v;

            response = VkApi.GetRequest(request);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<SaveWallPhotoResponseModel>(contentString);
        }

        public static string GetPhotoId()
        {
            GetUploadUrlModel model = ModelUtils.CreateGetUploadUrlModel(BaseTest.testData.UserId);
            GetUploadUrlResponseModel responseModel = ApiApplicationRequest.GetUploadUrl(model);

            UploadImage(responseModel);
            SaveWallPhotoModel saveWallPhotoModel = ModelUtils.CreateSaveWallPhotoModel();
            SaveWallPhotoResponseModel saveWallPhotoResponseModel = SaveWallPhoto(saveWallPhotoModel);

            return "photo" + saveWallPhotoModel.user_id + "_" + saveWallPhotoResponseModel.response[0].id;
        }

        public static WallCommentResponseModel AddCommentOnTheWall(WallCommentModel model)
        {
            BaseTest.Logger.Info("Send comment request");
            string request = host + apiMethods["addWallComment"] + "?" + "owner_id=" + model.owner_id + "&" +
                             "message=" + model.message + "&" + "post_id=" + model.post_id + "&" + "access_token=" + 
                             model.access_token + "&" + "v=" + model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);

            var httpContent = new StringContent(stringContent, Encoding.UTF8, ProjectConstants.MediaType);

            response = VkApi.PostRequest(request, httpContent);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<WallCommentResponseModel>(contentString);
        }
        //public static List<PostModel> GetAllPosts()
        //{
        //    Test.Log.Info("Get all posts");
        //    response = VkApi.GetRequest(postsPath);

        //    string contentString = response.Content.ReadAsStringAsync().Result;

        //    return JsonUtils.ReadJsonData<List<PostModel>>(contentString);
        //}

        //public static PostModel GetSpecifiedPost(int number)
        //{
        //    Test.Log.Info(string.Format("Get {0} post", number));
        //    response = VkApi.GetRequest(postsPath + number);

        //    string contentString = response.Content.ReadAsStringAsync().Result;

        //    return JsonUtils.ReadJsonData<PostModel>(contentString);
        //}

        //public static List<UserModel> GetAllUsers()
        //{
        //    Test.Log.Info("Get all users");
        //    response = VkApi.GetRequest(usersPath);

        //    string contentString = response.Content.ReadAsStringAsync().Result;

        //    return JsonUtils.ReadJsonData<List<UserModel>>(contentString);
        //}

        //public static UserModel GetSpecifiedUser(int number)
        //{
        //    Test.Log.Info(string.Format("Get {0} user", number));
        //    response = VkApi.GetRequest(usersPath + number);

        //    string contentString = response.Content.ReadAsStringAsync().Result;

        //    return JsonUtils.ReadJsonData<UserModel>(contentString);
        //}



        //public static bool CheckStatusCode(int expectedStatusCode)
        //{
        //    Test.Log.Info("Check status code");
        //    int statusCodeValue = (int)response.StatusCode;
        //    return statusCodeValue == expectedStatusCode;
        //}
    }
}
