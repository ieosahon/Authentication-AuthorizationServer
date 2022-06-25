using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.JobServices
{
    public interface IJobService
    {
        Task<Job> GetJobById(int jobId);
        Task<IEnumerable<Job>> GetJobs();
    }
}
