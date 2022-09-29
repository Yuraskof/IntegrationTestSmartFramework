namespace SmartVkApi.Models.ResponseModels
{
    public class GetLikesResponseModel
    {
        public Response response { get; set; }
        
        public class Response
        {
            public int count { get; set; }
            public User[] users { get; set; }
        }

        public class User
        {
            public string uid { get; set; }
            public int copied { get; set; }
        }

    }
}
