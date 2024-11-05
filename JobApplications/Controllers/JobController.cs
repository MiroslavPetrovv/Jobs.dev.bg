using Humanizer;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.DTOs.ViewModel;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

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

        

        public Job Get()
        {
            return this.data.Jobs.FirstOrDefault();
        }

        public IActionResult GetAll() => View(this.data.Jobs.Include(x=> x.Company).ToList());

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            int companyId = await companyService.GetByUserID(User.GetId());
            if(companyId == 0){
                // tempadata exception -> login 
            }
            JobFormDto job = new JobFormDto();
            job.CompanyId = companyId;

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
            await jobService.Add(job);
            return RedirectToAction("GetAll");
        }
        [Authorize]
        public async Task <IActionResult> Delete(int id)
        {
            await jobService.Delete(id);
            return RedirectToAction("GetAll");
        }
       [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //MAKE VALIDATION AND USE MAPPING!!!
            Job jobToUpdate =await data.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobToUpdate == null)
            {
                throw new InvalidOperationException("Job not founded");
            }
            JobEditViewModel jobEdit = new JobEditViewModel()
            { 
                Id = jobToUpdate.Id,
                CompanyId = jobToUpdate.CompanyId,
                Description = jobToUpdate.Description,
                Title = jobToUpdate.Title,
                IsAvaliable = jobToUpdate.IsAvaliable,
                Salary = jobToUpdate.Salary,
                WorkingHours = jobToUpdate.WorkingHours
            };
            


            return View(jobToUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobEditViewModel job)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Job? editedJob = await this.data.Jobs.FindAsync(job.Id);
            if (editedJob == null)
            {
                throw new InvalidOperationException("Invalid job offer created");
            }
            if (job.Salary <= 1000)
            {
                throw new ArgumentException("Salary cannot be under minimal wage");
            }
            //make a data validation for string length
            if (string.IsNullOrEmpty(job.Title) ||job.Title.Count() >= 100)
            {
                throw new ArgumentException("Invalid Title");
            }
            //make a limit for the description lenght
            if (!string.IsNullOrEmpty(job.Description))
            {
                throw new ArgumentException("Description cannot be empty");
            }
            if (job.WorkingHours <= 0)
            {
                throw new ArgumentException("Under minimal working hours");
            }




            editedJob.Id = job.Id;
            editedJob.CompanyId = job.CompanyId;
            editedJob.Description = job.Description;
            editedJob.Title = job.Title;
            editedJob.WorkingHours = job.WorkingHours;
            editedJob.Salary = job.Salary;
            editedJob.IsAvaliable = job.IsAvaliable;
            
            
            
            
            await this.data.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }
    }
}
