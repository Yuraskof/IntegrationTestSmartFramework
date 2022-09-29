namespace SmartVkApi.Models.ResponseModels
{
    public class WallCommentResponseModel
    {
        public Response response { get; set; }

        public class Response
        {
            public int comment_id { get; set; }
            public object[] parents_stack { get; set; }
        }
    }
}
