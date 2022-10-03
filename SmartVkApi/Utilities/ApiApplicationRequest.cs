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
        private static string host = BaseTest.testData.Host;

        public static WallPostResponseModel CreatePostOnTheWall(WallPostModel model)
        {
            LoggerUtils.LogStep(nameof(CreatePostOnTheWall) + " \"Send post\"");
            
            string request = host + apiMethods["postOnTheWall"] + "?"+ "owner_id=" + model.owner_id + "&" +
                             "message="+model.message + "&" + "access_token=" + model.access_token + "&" + "v=" +
                             model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, FileConstants.MediaType);
            HttpResponseMessage response = ApiUtils.PostRequest(request, httpContent);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(CreatePostOnTheWall) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<WallPostResponseModel>(contentString);
        }

        public static WallPostResponseModel EditPostOnTheWall(WallPostModel model)
        {
            LoggerUtils.LogStep(nameof(EditPostOnTheWall) + " \"Send edited post\"");
            string request = host + apiMethods["editPostOnTheWall"] + "?" + "owner_id=" + model.owner_id + "&" +
                             "post_id="+ model.post_id +"&"+ "message=" + model.message + "&" + "attachments=" + model.attachments + 
                             "&" + "access_token=" + model.access_token + "&" + "v=" + model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, FileConstants.MediaType);
            HttpResponseMessage response = ApiUtils.PostRequest(request, httpContent);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(EditPostOnTheWall) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<WallPostResponseModel>(contentString);
        }

        public static GetUploadUrlResponseModel GetUploadUrl(GetUploadUrlModel model)
        {
            LoggerUtils.LogStep(nameof(GetUploadUrl) + " \"Send Get Upload Url request\"");
            string request = host + apiMethods["getUploadUrl"] + "?" + "group_id=" + model.group_id + "&" +
                             "access_token=" + model.access_token + "&" + "v=" + model.v;

            HttpResponseMessage response = ApiUtils.GetRequest(request);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(GetUploadUrl) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<GetUploadUrlResponseModel>(contentString);
        }

        public static async void UploadImage(GetUploadUrlResponseModel responseModel)
        {
            LoggerUtils.LogStep(nameof(UploadImage) + " \"Send \"Upload image\" request\"");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(FileReader.ReadImage(FileConstants.PathToWallPostImage), "photo", "postImage.png");
            HttpResponseMessage response = ApiUtils.PostRequest(responseModel.response.upload_url, multipartContent);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(UploadImage) + $" \"Invalid status code - [{response.StatusCode}]\"");
            }
            var httpContent =  await response.Content.ReadAsByteArrayAsync();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string responseString = Encoding.GetEncoding(1251).GetString(httpContent, 0, httpContent.Length);
            ModelUtils.uploadImageResponse = JsonUtils.ReadJsonData<UploadImageResponseModel>(responseString);
        }

        public static SaveWallPhotoResponseModel SaveWallPhoto(SaveWallPhotoModel model)
        {
            LoggerUtils.LogStep(nameof(SaveWallPhoto) + "Send \"Save photo\" request");
            string request = host + apiMethods["saveWallPhoto"] + "?" + "group_id=" + model.user_id + "&" +
                             "photo=" + model.photo + "&" + "server=" + model.server + "&" + "hash=" + model.hash + "&" +
                             "access_token=" + model.access_token + "&" + "v=" + model.v;

            HttpResponseMessage response = ApiUtils.GetRequest(request);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(SaveWallPhoto) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<SaveWallPhotoResponseModel>(contentString);
        }

        public static string GetPhotoId()
        {
            LoggerUtils.LogStep(nameof(GetPhotoId) + " \"Start creating photoId\"");
            try
            {
                GetUploadUrlModel model = ModelUtils.CreateGetUploadUrlModel(BaseTest.testData.UserId);
                GetUploadUrlResponseModel responseModel = GetUploadUrl(model);
                UploadImage(responseModel);
                SaveWallPhotoModel saveWallPhotoModel = ModelUtils.CreateSaveWallPhotoModel();
                SaveWallPhotoResponseModel saveWallPhotoResponseModel = SaveWallPhoto(saveWallPhotoModel);
                return "photo" + saveWallPhotoModel.user_id + "_" + saveWallPhotoResponseModel.response[0].id;
            }
            catch(Exception ex)
            {
                LoggerUtils.LogError("Can't get photo id", ex);
                return null;
            }
        }

        public static WallCommentResponseModel AddCommentOnTheWall(WallCommentModel model)
        {
            LoggerUtils.LogStep(nameof(AddCommentOnTheWall) + " \"Send comment request\"");
            string request = host + apiMethods["addWallComment"] + "?" + "owner_id=" + model.owner_id + "&" +
                             "message=" + model.message + "&" + "post_id=" + model.post_id + "&" + "access_token=" + 
                             model.access_token + "&" + "v=" + model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, FileConstants.MediaType);
            HttpResponseMessage response = ApiUtils.PostRequest(request, httpContent);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(AddCommentOnTheWall) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<WallCommentResponseModel>(contentString);
        }

        public static GetLikesResponseModel GetLikesFromTheWallPost(GetLikesRequestModel model)
        {
            LoggerUtils.LogStep(nameof(GetLikesFromTheWallPost) + " \"Send get likes request\"");

            string request = host + apiMethods["getLikesFromWallPost"] + "?" + "owner_id=" + model.owner_id + "&" +
                             "post_id=" + model.post_id + "&" + "access_token=" +
                             model.access_token + "&" + "v=" + model.v;

            HttpResponseMessage response = ApiUtils.GetRequest(request);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(GetLikesFromTheWallPost) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<GetLikesResponseModel>(contentString);
        }

        public static DeletePostFromWallResponseModel DeletePost(DeletePostFromWallModel model)
        {
            LoggerUtils.LogStep(nameof(DeletePost) + " \"Send delete post request\"");
            
            string request = host + apiMethods["deleteWallPost"] + "?" + "owner_id=" + model.owner_id + "&" +
                             "post_id=" + model.post_id + "&" + "access_token=" +
                             model.access_token + "&" + "v=" + model.v;

            HttpResponseMessage response = ApiUtils.GetRequest(request);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(DeletePost) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<DeletePostFromWallResponseModel>(contentString);
        }

        public static bool CheckStatusCode(int expectedStatusCode, HttpResponseMessage response)
        {
            LoggerUtils.LogStep(nameof(CheckStatusCode) + " \"Check status code\"");
            return (int)response.StatusCode == expectedStatusCode;
        }
    }
}
