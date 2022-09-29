namespace SmartVkApi.Models.RequestModels
{
    public class SaveWallPhotoModel
    {
        public string user_id { get; set; }
        public string server { get; set; }
        public string photo { get; set; }
        public string hash { get; set; }
        public string v { get; set; }
        public string access_token { get; set; }
    }
}
