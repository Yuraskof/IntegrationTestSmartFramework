namespace SmartVkApi.Models.RequestModels
{
    public class WallPostModel
    {
        public string owner_id { get; set; }
        public string message { get; set; }
        public string post_id { get; set; }
        public string v { get; set; }
        public string access_token { get; set; }
        public string attachments { get; set; }
    }
}
