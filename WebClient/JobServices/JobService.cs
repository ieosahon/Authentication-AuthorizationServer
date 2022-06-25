using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Config;
using WebClient.Http;
using WebClient.Models;

namespace WebClient.JobServices
{
    public class JobService : IJobService
    {
        private readonly IHttpClient _httpClient;
        private readonly ApiConfig _apiConfig;

        public JobService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<Job> GetJobById(int jobId)
        {
            var dataString = await _httpClient.GetStringAsync(_apiConfig.JobsApiUrl + "/jobs/" + jobId);
            return JsonConvert.DeserializeObject<Job>(dataString);
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var dataString = await _httpClient.GetStringAsync(_apiConfig.JobsApiUrl + "/jobs/" );
            return JsonConvert.DeserializeObject<IEnumerable<Job>>(dataString);
        }
    }
}
