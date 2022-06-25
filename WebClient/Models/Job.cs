using System;

namespace WebClient.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Location { get; set; }

    }
}
