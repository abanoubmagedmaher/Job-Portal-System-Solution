using Microsoft.AspNetCore.Identity;

namespace Job_Portal_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

    }
}
