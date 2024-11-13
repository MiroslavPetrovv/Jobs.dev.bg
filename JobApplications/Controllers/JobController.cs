
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService; 
        private ApplicationDbContext data;

        public JobController(ApplicationDbContext context,ICompanyService companyService, IJobService jobService)
        {
            data = context;
            this.jobService = jobService;
            this.companyService = companyService;
        }

        

        public async Task<Job> Get()
        {
            return await this.data.Jobs.FirstOrDefaultAsync();
        }

        public IActionResult GetAll() => View(this.data.Jobs.Include(x=> x.Company).ToList());

        [HttpGet]
        public async Task<IActionResult> Add()
        {
          
            int companyId = await companyService.GetByUserID(User.GetId());
            if(companyId == 0){
                TempData["ErrorNotAuth"] = "You should log in in your profile first!";

                return RedirectToAction("Index", "Home"); // Login
            }
            JobFormDto job = new JobFormDto();
            

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobFormDto job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                //throw new ArgumentException("Invalid Data");
            }
            int companyId = await companyService.GetByUserID(User.GetId());
            if(companyId == 0)
            {
                throw new ArgumentException("User was lost");
            }
            job.CompanyId = companyId;

            await jobService.Add(job);

            return RedirectToAction("GetAll");
        }
        [Authorize]
        public async Task <IActionResult> Delete(int id)
        {
            //add if statements
            await jobService.Delete(id, User.GetId());
            return RedirectToAction("GetAll");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //MAKE VALIDATION AND USE MAPPING!!!
            Job? jobToUpdate =await data.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobToUpdate == null)
            {
                throw new InvalidOperationException("Job not founded");
            }

            JobFormDto jobEdit = new JobFormDto()
            { 
                Id = jobToUpdate.Id,
                CompanyId = jobToUpdate.CompanyId,
                Description = jobToUpdate.Description,
                Title = jobToUpdate.Title,
                IsAvaliable = jobToUpdate.IsAvaliable,
                Salary = jobToUpdate.Salary,
                WorkingHours = jobToUpdate.WorkingHours,
                Banner = jobToUpdate.Banner,
                
            };
            


            return View(jobEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobFormDto job)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await jobService.Edit(job);
            return RedirectToAction("GetAll");
        }

       
    }
}
