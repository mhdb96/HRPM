using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KDAUILibrary.Helpers
{
    public class ApiHelper
    {
        private HttpClient apiClient;
        private static readonly ApiHelper _instance = new ApiHelper();
        private ApiHelper()
        {
            IntializeClient();
        }
        public static ApiHelper GetApiHelper()
        {
            return _instance;
        }
        private void IntializeClient()
        {
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri("https://localhost:44301/");
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> PostFile(BinaryFile file)
        {
            using (HttpResponseMessage res = await apiClient.PostAsJsonAsync("api/files/UploadFile", file))
            {
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> PostLog(Log log)
        {
            using (HttpResponseMessage res = await apiClient.PostAsJsonAsync("api/logs/postlog", log))
            {
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> GetServerStatus()
        {
            using (HttpResponseMessage res = await apiClient.GetAsync("api/checkers/isalive"))
            {
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
