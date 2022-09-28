using SmartVkApi.Base;

namespace SmartVkApi.Utilities
{
    public class VkApi
    {
        public static HttpResponseMessage GetRequest(string request)
        {
            BaseTest.Logger.Info(string.Format("Get request {0}", request));

            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync(request).Result;

            client.Dispose();

            return response;
        }

        public static HttpResponseMessage PostRequest(string request, HttpContent content) 
        {
            BaseTest.Logger.Info(string.Format("Post request {0}", request));
            
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.PostAsync(request, content).Result;

            client.Dispose();

            return response;
        }
    }
}
