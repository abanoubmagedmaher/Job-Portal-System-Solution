using Job_Portal_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_System.Data
{
    public class JobPortalContext : DbContext
    {
        public DbSet<Job> Jops { get; set; }
        public DbSet<Application> Applications { get; set; }
        public JobPortalContext():base()
        {
            
        }

        public JobPortalContext(DbContextOptions<JobPortalContext> options):base(options)
        {
            
        }

    }
}
