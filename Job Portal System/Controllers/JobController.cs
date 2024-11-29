using Job_Portal_System.Interfaces;
using Job_Portal_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _unitOfWork.JobRepository.GetAllAsync();
            if (jobs == null)
            {
                return NotFound(new { Message = "Jobs not Found" });
            }
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _unitOfWork.JobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound(new { Message = "Job not Found" });
            }
            return Ok(job);

        }
    }
}
