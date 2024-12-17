using AutoMapper;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.User.Controllers
{
    public class JobController: UserController
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;
        private readonly ISavedJobService savedJobService;

        public JobController(ICompanyService companyService, IJobService jobService, IApplicationService applicationService, IMapper mapper, ISavedJobService savedJobService)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
            this.mapper = mapper;
            this.savedJobService = savedJobService;
        }
        //See all jobs that are Available
        //See saved jobs
        //Apply for a job
        //See his application for the job
        //See all his applications

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    if (User.GetId() == null)
        //    {
        //        TempData["ErrorNotAuth"] = "You should log in with your company first!";

        //        return RedirectToAction("Index", "Home"); // Login
        //    }
        //    var jobs = await this.jobService.GetAllAvailableJobs();
        //    return View(jobs);
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] JobFilterParams filterParams)
        {
            // Get the filtered list of jobs
            
            var filteredJobs = await jobService.FilterJobsAsync(filterParams);

            // Return the filtered jobs to the view
           var mappedJobs = mapper.Map<List<JobFormDto>>(filteredJobs);
            return View(mappedJobs);
        }
        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> SaveJobAsync(int jobId)
        {
            var userId = User.GetId();

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "You must be logged in to save a job.";
                return RedirectToAction("GetAll");
            }

            try
            {
                await savedJobService.SaveJobAsync(userId, jobId); 
                TempData["SuccessMessage"] = "Job saved successfully!";
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., job already saved)
                TempData["Error"] = "An error occurred while saving the job.";
            }

            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSavedJobsAsync()
        {
            var userId = User.GetId(); // Assuming you have a method to get the logged-in user's ID
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "You must be logged in to view saved jobs.";
                return RedirectToAction("Index", "Home");
            }

            var savedJobs = await savedJobService.GetSavedJobsAsync(userId);
            return View(savedJobs);
        }
    }
}
