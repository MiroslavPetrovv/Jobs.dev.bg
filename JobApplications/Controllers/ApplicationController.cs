using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace JobApplications.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IApplicationService applicationService;
        private readonly ILogger<HomeController> _logger;
        private readonly IHostEnvironment _hostEnvironment;


        public ApplicationController(ICompanyService companyService, IJobService jobService,
            IApplicationService applicationService, IHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> ApplyForJob(int id)
        {
            var job = await jobService.GetJobByIdAsync(id);
            if (string.IsNullOrEmpty(User.GetId()))
            {
                //to return error
                TempData["ErrorNotAuth"] = "You should log in in your profile first!";

                return RedirectToAction("Index", "Home"); // Login
            }

            var applicationDto = new ApplicationFormDto
            {
                IdentityUserId = User.GetId(),
                JobId = id,
                Job = job,
                StatusId = 1
            };

            return View(applicationDto);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyForJob(ApplicationFormDto application,List<IFormFile> CvFile)
        {
            try
            {
                var userId = User.GetId();
                await applicationService.ApplyForAJobAsync(application, userId,CvFile);

                TempData["SuccessMessage"] = "Your application has been submitted successfully!";
                return RedirectToAction("GetAll", "Job");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while applying for the job.");
                return StatusCode(500, "An error occurred while processing your application.");
            }

        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCv(string relativePath)
        {
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, relativePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = "application/pdf";
            return File(fileBytes, contentType, Path.GetFileName(fullPath));
        }

        //[HttpGet("DownloadCv/{applicationId}")]
        //[Authorize]
        //public async Task<IActionResult> DownloadCv(int applicationId)
        //{
        //    var application = await this.applicationService.GetApplicationByIdForDownloadingCv(applicationId);

        //    if (application == null || application.CvFileData == null)
        //    {
        //        return NotFound("CV not found for the given application.");
        //    }

        //    Assuming CvFilePath is a byte array
        //    byte[] fileData = application.CvFileData; // You might need to read it from a file if it's a path
        //    return File(fileData, "application/pdf", $"CV_{applicationId}.pdf");
        //}

        //[HttpPost("UpdateStatus/{applicationId}")]
        //public async Task<IActionResult> UpdateApplicationStatus(int applicationId, [FromBody] UpdateStatusDto dto)
        //{
        //    try
        //    {
        //        var success = await applicationService.UpdateApplicationStatusAsync(applicationId, dto.StatusName);

        //        if (success)
        //        {
        //            return Ok("Status updated successfully.");
        //        }
        //        else
        //        {
        //            return BadRequest("Failed to update status.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}


    }
}
