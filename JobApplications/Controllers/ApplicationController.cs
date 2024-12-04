using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IApplicationService applicationService;
        private readonly ILogger<HomeController> _logger;


        public ApplicationController(ICompanyService companyService, IJobService jobService, IApplicationService applicationService)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
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
        public async Task<IActionResult> ApplyForJob(ApplicationFormDto application)
        {
            try
            {
                var userId = User.GetId(); // Assuming GetId() retrieves the logged-in user's ID
                await applicationService.ApplyForAJobAsync(application, userId);

                TempData["SuccessMessage"] = "Your application has been submitted successfully!";
                return RedirectToAction("JobDetails", new { id = application.JobId });
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




    }
}
