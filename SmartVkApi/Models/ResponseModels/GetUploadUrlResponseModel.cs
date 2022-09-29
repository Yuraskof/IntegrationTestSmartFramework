namespace SmartVkApi.Models.ResponseModels
{
    public class GetUploadUrlResponseModel
    {
        public Response response { get; set; }

        public class Response
        {
            public string upload_url { get; set; }
            public int album_id { get; set; }
            public int user_id { get; set; }
        }
    }
}
