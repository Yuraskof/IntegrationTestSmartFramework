namespace SmartVkApi.Models.ResponseModels
{
    public class WallPostResponseModel
    {
        public Response response { get; set; }

        public class Response
        {
            public string post_id { get; set; }
        }
    }
}
