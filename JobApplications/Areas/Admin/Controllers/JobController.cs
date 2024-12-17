using AutoMapper;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.Admin.Controllers
{
    public class JobController : BaseAdminController
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
        [HttpPost]
        public IActionResult RemoveJob(int id)
        {
            var job = jobService.GetJobByIdAsync(id);
            if (job != null)
            {
                jobService.Delete(id,User.GetId());
                return RedirectToAction("GetAllJobs"); // Redirect to the job listings page after removal
            }
            return NotFound();
        }
    }
}
