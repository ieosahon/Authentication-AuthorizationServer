using System;
using System.ComponentModel.DataAnnotations;

namespace JobsAppApi.Models
{
    public class Job
    {
        [Key]
        public int WorkId { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
