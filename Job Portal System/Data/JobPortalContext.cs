using Job_Portal_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace Job_Portal_System.Data
{
    public class JobPortalContext : IdentityDbContext<ApplicationUser>
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
