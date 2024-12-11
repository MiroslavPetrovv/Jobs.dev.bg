using AutoMapper;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Areas.HR.Controllers
{
    public class JobController : BaseController
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jobs = await this.jobService.GetAllAsync();
                return View(jobs);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while fetching the job list. Please try again later.";
                // Log the exception (optional)
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                int companyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
                if (companyId == 0)
                {
                    TempData["ErrorNotAuth"] = "You should log in to your profile first!";
                    return RedirectToAction("Index", "Home"); // Login
                }

                JobFormDto job = new JobFormDto();
                return View(job);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while preparing the add job form.";
                // Log the exception (optional)
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobFormDto job)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid data provided.";
                    return View(job);
                }

                int companyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
                if (companyId == 0)
                {
                    TempData["Error"] = "You should log in to your profile first!";
                    return RedirectToAction("Index", "Home");
                }

                job.CompanyId = companyId;
                await jobService.AddAsync(job);

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while adding the job.";
                // Log the exception (optional)
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await jobService.Delete(id, User.GetId());
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the job.";
                // Log the exception (optional)
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorInvalidCompany"] = "You should provide only valid information.";
                    return View();
                }

                Job? jobToUpdate = await this.jobService.GetJobByIdAsync(id);
                if (jobToUpdate == null)
                {
                    TempData["Error"] = "Job not found.";
                    return RedirectToAction("GetAll");
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
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while preparing the edit form.";
                // Log the exception (optional)
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobFormDto job)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid data provided.";
                    return View(job);
                }

                await jobService.EditAsync(job);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the job.";
                // Log the exception (optional)
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
