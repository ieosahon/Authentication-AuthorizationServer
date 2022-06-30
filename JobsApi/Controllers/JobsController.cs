using JobsApi.Data;
using JobsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class JobsController : ControllerBase
    {
        private readonly JobsDbContext _context;
        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger, JobsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Job>> GetAllJob()
        {
            return await _context.Jobs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Job> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return null;
            }

            return job;
        }
    }
}
