using System.ComponentModel.DataAnnotations;

namespace Job_Portal_System.DTOS
{
    public class ApplicationDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        public IFormFile Resume { get; set; } // For file uploads
    }
}
