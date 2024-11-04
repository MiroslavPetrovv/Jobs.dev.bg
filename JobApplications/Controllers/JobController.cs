using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Delete(int id)
        {
            Job jobToDelete = data.Jobs.FirstOrDefault(x => x.Id == id);
            data.Jobs.Remove(jobToDelete);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }
       [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Job jobToUpdate = data.Jobs.FirstOrDefault(x => x.Id == id);


            return View(jobToUpdate);
        }
        [HttpPost]
        public IActionResult Edit(Job job)
        {
            if (String.IsNullOrEmpty(job.Title))
            {

            }
            if (String.IsNullOrEmpty(job.Salary.ToString()))
            {

            }
            
            data.Jobs.Update(job);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }
    }
}
