using Newtonsoft.Json.Linq;
using NUnit.Framework.Internal;
using SmartVkApi.Base;
using SmartVkApi.Constants;

namespace SmartVkApi.Utilities
{
    public static class FileReader
    {
        public static Dictionary<string, string> GetApiMethods()
        {
            BaseTest.Logger.Info("Get api methods");
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
                BaseTest.Logger.Info("Log file deleted");
            }
        }
        
        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                BaseTest.Logger.Info(string.Format("File {0} read", path));
                return sr.ReadToEnd();
            }
        }
    }
}