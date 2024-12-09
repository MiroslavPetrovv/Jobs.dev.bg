using JobApplications.Controllers;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.User.Controllers
{
    public class ApplicationController : UserController
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
        public async Task<IActionResult> ApplyForJob(ApplicationFormDto application, List<IFormFile> CvFile)
        {
            try
            {
                var userId = User.GetId();
                await applicationService.ApplyForAJobAsync(application, userId, CvFile);

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
        public async Task<IActionResult> SeeAllApplicationByUserId(string userId)
        {
            if (User.GetId() == null)
            {
                TempData["ErrorNotAuth"] = "You should log in with your company first!";

                return RedirectToAction("Index", "Home"); // Login
            }

            var applications = await this.applicationService.SeeAllApplicationByUserId(userId);
            return View(applications);
        }
    }
}
