using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartVkApi.Utilities
{
    public static class JsonUtils
    {
        public static JObject ParseToJsonObject(string content)
        {
            LoggerUtils.LogStep(nameof(ParseToJsonObject) + " \"Start parsing to json object\"");
            return JObject.Parse(content);
        }

        public static T ReadJsonData<T>(string content)
        {
            LoggerUtils.LogStep(nameof(ReadJsonData) + " \"Start deserializing\"");
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T ReadJsonDataFromPath<T>(string path)
        {
            LoggerUtils.LogStep(nameof(ReadJsonDataFromPath) + $" \"Path - [{path}] deserialized\"");
            return JsonConvert.DeserializeObject<T>(FileReader.ReadFile(path));
        }

        public static string SerializeJsonData(object content)
        {
            LoggerUtils.LogStep(nameof(SerializeJsonData) + " \"Start serializing\"");
            return JsonConvert.SerializeObject(content);
        }
    }
}
