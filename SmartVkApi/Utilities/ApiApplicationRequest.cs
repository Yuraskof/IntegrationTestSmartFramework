using NUnit.Framework.Internal;
using SmartVkApi.Base;
using SmartVkApi.Models;

namespace SmartVkApi.Utilities
{
    public class ApiApplicationRequest
    {
        public static Dictionary<string, string> apiMethods = FileReader.GetApiMethods();

        public static HttpResponseMessage response;
        
        public static WallPostResponseModel CreatePostOnTheWall(WallPostModel model)
        {
            BaseTest.Logger.Info("Send post");
            string request = apiMethods["postOnTheWall"] + "?"+ "owner_id=" + model.owner_id + "&" +
                             "message=" + model.message + "&" + "access_token=" + model.access_token + "&" + "v=" +
                             model.v;

            response = VkApi.PostRequest(request, model);

            string contentString = response.Content.ReadAsStringAsync().Result;

            return JsonUtils.ReadJsonData<WallPostResponseModel>(contentString);
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
