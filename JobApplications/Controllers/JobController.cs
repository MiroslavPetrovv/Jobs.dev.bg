using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;


        public JobController(ICompanyService companyService, IJobService jobService)
        {

            this.jobService = jobService;
            this.companyService = companyService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] JobFilterParams filterParams)
        {
            // Get the filtered list of jobs
            List<Job> jobs = await this.jobService.GetAllAsync();
            var filteredJobs = await jobService.FilterJobsAsync(filterParams);

            // Return the filtered jobs to the view
            return View(filteredJobs);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    if (User.GetId() == null)
        //    {
        //        TempData["ErrorNotAuth"] = "You should log in with your company first!";

        //        return RedirectToAction("Index", "Home"); // Login
        //    }
        //    var jobs = await this.jobService.GetAllAsync();
        //    return View(jobs);
        //}
        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> Add()
        //{

        //    int companyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
        //    if (companyId == 0) {
        //        TempData["ErrorNotAuth"] = "You should log in with your company first!";

        //        return RedirectToAction("Index", "Home"); // Login
        //    }
        //    JobFormDto job = new JobFormDto();


        //    return View(job);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(JobFormDto job)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //        //throw new ArgumentException("Invalid Data");
        //    }
        //    int companyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
        //    if (companyId == 0)
        //    {
        //        throw new ArgumentException("User was lost");
        //    }
        //    job.CompanyId = companyId;

        //    await jobService.AddAsync(job);

        //    return RedirectToAction("GetAll");
        //}
        //[Authorize]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    //add if statements
        //    await jobService.Delete(id, User.GetId());
        //    return RedirectToAction("GetAll");
        //}
        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["ErrorInvalidCompany"] = "You should type only valid inforamtion";
        //    }
        //    Job? jobToUpdate = await this.jobService.GetJobByIdAsync(id);
        //    if (jobToUpdate == null)
        //    {
        //        throw new InvalidOperationException("Job not founded");
        //    }

        //    JobFormDto jobEdit = new JobFormDto()
        //    {
        //        Id = jobToUpdate.Id,
        //        CompanyId = jobToUpdate.CompanyId,
        //        Description = jobToUpdate.Description,
        //        Title = jobToUpdate.Title,
        //        IsAvaliable = jobToUpdate.IsAvaliable,
        //        Salary = jobToUpdate.Salary,
        //        WorkingHours = jobToUpdate.WorkingHours,
        //        Banner = jobToUpdate.Banner,

        //    };



        //    return View(jobEdit);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(JobFormDto job)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    await jobService.EditAsync(job);
        //    return RedirectToAction("GetAll");
        //}




    }
}
