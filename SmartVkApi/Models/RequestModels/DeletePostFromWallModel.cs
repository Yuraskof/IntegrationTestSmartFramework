namespace SmartVkApi.Models.RequestModels
{
    public class DeletePostFromWallModel
    {
        public string owner_id { get; set; }
        public string post_id { get; set; }
        public string v { get; set; }
        public string access_token { get; set; }
    }
}
