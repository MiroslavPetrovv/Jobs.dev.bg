
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.DTOs.ViewModel.JobViewModels;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.Design;
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
                WorkingHours = jobToUpdate.WorkingHours
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
