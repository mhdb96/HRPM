using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HRPMUILibrary.Helpers
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
            apiClient.BaseAddress = new Uri("https://localhost:44360/");
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> PostFile(BinaryFile file, User user)
        {
            file.User = user;
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
        public async Task<bool> PostLog(Log log, User user)
        {
            log.User = user;
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
        public async Task<List<Department>> GetDepartments()
        {
            using (HttpResponseMessage res = await apiClient.GetAsync("api/departments/getall"))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<List<Department>>();                    
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<User>> GetUsersByDepartment(int id)
        {
            using (HttpResponseMessage res = await apiClient.GetAsync($"api/users/getbydepid/{id}"))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<List<User>>();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> GetUserPassword(User user)
        {
            using (HttpResponseMessage res = await apiClient.PostAsJsonAsync("api/users/checkpassword", user))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> PostTask(WorkTask task, User user)
        {
            task.User = user;
            using (HttpResponseMessage res = await apiClient.PostAsJsonAsync("api/tasks/PostTask", task))
            {
                if (res.IsSuccessStatusCode)
                {
                    task = null;
                    return await res.Content.ReadAsAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> PostUsageTime(UsageTime usage, User user)
        {
            usage.User = user;
            using (HttpResponseMessage res = await apiClient.PostAsJsonAsync("api/usagetimes/postusage", usage))
            {
                if (res.IsSuccessStatusCode)
                {
                    usage = null;
                    return await res.Content.ReadAsAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
