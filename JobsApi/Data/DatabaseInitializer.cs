using JobsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobsApi.Data
{
    public class DatabaseInitializer
    {
        /// <summary>
        /// This method will be called from the configure method of the startUp class
        /// </summary>
        /// <param name="jobsDbContext"></param>
        public static void Initializer(JobsDbContext jobsDbContext)
        {
            jobsDbContext.Database.EnsureCreated(); // to check if the db for the context exist, else the db is created
            if (jobsDbContext.Jobs.Any()) return;

            var jobs = new List<Job>
            {
                new Job
                {
                    Title = "Software Engineer",
                    Description = "We are looking for a software engineer to join our team.",
                    Location = "New York, NY",
                    Company = "Apple",
                    AssignedDate = DateTime.Now,
                },

                new Job
                {
                    Title = "Jnr. Software Engineer",
                    Description = "We are looking for a software engineer to join our team.",
                    Location = "Lagos, Nigeria",
                    Company = "Microsoft",
                    AssignedDate = DateTime.Now,
                },

                new Job
                {
                    Title = "Cloud Engineer",
                    Description = "We are looking for a software engineer to join our team.",
                    Location = "Accra, Ghana",
                    Company = "Twitter",
                    AssignedDate = DateTime.Now,
                }
            };
            jobsDbContext.Jobs.AddRange(jobs);
            jobsDbContext.SaveChanges();
        }
    }
}
