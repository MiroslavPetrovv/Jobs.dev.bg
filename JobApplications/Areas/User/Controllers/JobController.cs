using AutoMapper;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.User.Controllers
{
    public class JobController: UserController
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;


        public JobController(ICompanyService companyService, IJobService jobService, IApplicationService applicationService, IMapper mapper)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
            this.mapper = mapper;
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
        public async Task<IActionResult> GetAll([FromQuery] JobFilterParams filterParams)
        {
            // Get the filtered list of jobs
            
            var filteredJobs = await jobService.FilterJobsAsync(filterParams);

            // Return the filtered jobs to the view
            var mappedJobs = mapper.Map<List<JobFormDto>>(filteredJobs);
            return View(mappedJobs);
        }
    }
}
