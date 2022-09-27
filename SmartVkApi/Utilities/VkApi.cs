using System.Text;
using Newtonsoft.Json;
using SmartVkApi.Base;
using SmartVkApi.Constants;

namespace SmartVkApi.Utilities
{
    public class VkApi
    {
        private static string host = BaseTest.testData.Host; 

        public static HttpResponseMessage GetRequest(string request)
        {
            BaseTest.Logger.Info(string.Format("Get request {1}{0}", request, host));

            HttpClient client = new HttpClient();

            string uri = host + request; 

            HttpResponseMessage response = client.GetAsync(uri).Result;

            client.Dispose();

            return response;
        }

        public static HttpResponseMessage PostRequest(string request, object content) 
        {
            BaseTest.Logger.Info(string.Format("Post request {1}{0}", request, host));

            var stringContent = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, ProjectConstants.MediaType);

            HttpClient client = new HttpClient();

            string uri = host + request;

            HttpResponseMessage response = client.PostAsync(uri, httpContent).Result; 

            //var result =  response.Content.ReadAsStringAsync().Result; 
            
            //var mod = JsonUtils.ReadJsonData<WallPostResponseModel>(result);

            client.Dispose();

            return response;
        }
    }
}
