using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartVkApi.Base;

namespace SmartVkApi.Utilities
{
    public static class JsonUtils
    {
        public static JObject ParseToJsonObject(string content)
        {
            BaseTest.Logger.Info("Start parsing to json object");
            return JObject.Parse(content);
        }

        public static T ReadJsonData<T>(string content)
        {
            BaseTest.Logger.Info("Start deserializing");
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T ReadJsonDataFromPath<T>(string path)
        {
            BaseTest.Logger.Info(string.Format("Path {0} deserialized", path));
            return JsonConvert.DeserializeObject<T>(FileReader.ReadFile(path));
        }
    }
}
