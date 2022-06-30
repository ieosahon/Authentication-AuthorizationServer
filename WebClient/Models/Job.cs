using System;

namespace WebClient.Models
{
    public class Job
    {
        public int WorkId { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }

    }
}
