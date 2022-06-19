using JobsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Data
{
    public class JobsDbContext : DbContext
    {
        public JobsDbContext(DbContextOptions<JobsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().ToTable("Jobs");
        }

        public DbSet<Job> Jobs { get; set; }
    }
}
