namespace SmartVkApi.Models.ResponseModels
{
    public class SaveWallPhotoResponseModel
    {
        public Response[] response { get; set; }

        public class Response
        {
            public int album_id { get; set; }
            public int date { get; set; }
            public int id { get; set; }
            public int owner_id { get; set; }
            public string access_key { get; set; }
            public Size[] sizes { get; set; }
            public string text { get; set; }
            public bool has_tags { get; set; }
        }

        public class Size
        {
            public int height { get; set; }
            public string url { get; set; }
            public string type { get; set; }
            public int width { get; set; }
        }

    }
}
