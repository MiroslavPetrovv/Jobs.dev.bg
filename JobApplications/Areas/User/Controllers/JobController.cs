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


        public JobController(ICompanyService companyService, IJobService jobService, IApplicationService applicationService)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
        }
        //See all jobs that are Available
        //See saved jobs
        //Apply for a job
        //See his application for the job
        //See all his applications
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (User.GetId() == null)
            {
                TempData["ErrorNotAuth"] = "You should log in with your company first!";

                return RedirectToAction("Index", "Home"); // Login
            }
            var jobs = await this.jobService.GetAllAvailableJobs();
            return View(jobs);
        }
    }
}
