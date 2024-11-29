namespace Job_Portal_System.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ResumePath { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
