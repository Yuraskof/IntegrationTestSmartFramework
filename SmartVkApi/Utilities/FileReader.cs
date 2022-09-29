using Newtonsoft.Json.Linq;
using SmartVkApi.Constants;

namespace SmartVkApi.Utilities
{
    public static class FileReader
    {
        public static Dictionary<string, string> GetApiMethods()
        {
            LoggerUtils.LogStep(nameof(GetApiMethods) + " \"Get api methods\"");
            
            var filePath = ProjectConstants.PathToApiMethods;
            var json = File.ReadAllText(filePath);
            var jsonObj = JObject.Parse(json);

            Dictionary<string, string> apiMethods = new Dictionary<string, string>();

            foreach (var element in jsonObj)
            {
                apiMethods.Add(element.Key, element.Value.ToString());
            }
            return apiMethods;
        }

        public static void ClearLogFile()
        {
            FileInfo file = new FileInfo(ProjectConstants.PathToLogFile);

            if (file.Exists)
            {
                file.Delete();
                LoggerUtils.LogStep(nameof(ClearLogFile) + $" \"Log file deleted - [{file}]\"");
            }
        }

        public static ByteArrayContent ReadImage(string path)
        {
            LoggerUtils.LogStep(nameof(ReadImage) + $" \"Image - [{path}] read\"");
            byte[] imgdata = File.ReadAllBytes(path);
            return new ByteArrayContent(imgdata);
        }

        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                LoggerUtils.LogStep(nameof(ReadFile) + $" \"File - [{path}] read\"");
                return sr.ReadToEnd();
            }
        }
    }
}