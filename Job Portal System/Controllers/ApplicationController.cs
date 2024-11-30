using Job_Portal_System.DTOS;
using Job_Portal_System.Interfaces;
using Job_Portal_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitApplication([FromForm] ApplicationDto applicationDto)
        {
            // Validate Job Id
            var job = await _unitOfWork.JobRepository.GetByIdAsync(applicationDto.JobId);
            if (job == null)
            {
                return NotFound( new { Message="Job not Found" });
            }

            // save File
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if(!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            //path
            string filePath = Path.Combine(uploadsFolder, Guid.NewGuid() + Path.GetExtension(applicationDto.Resume.FileName));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await applicationDto.Resume.CopyToAsync(stream);
            }

            // Map DTo To Application Entity
            var application = new Application
            {
                Name =applicationDto.Name,
                Email = applicationDto.Email,
                ResumePath =filePath,
                JobId = applicationDto.JobId
            };

            await _unitOfWork.ApplicationRepository.AddAsync(application);
            await _unitOfWork.SaveAsync();
            return Ok(new { Message="Application Submitted SuccessFully !" });


        }
    }
}
