using AutoMapper;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.HR.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IMapper mapper;
        private readonly ICompanyService companyService;
        private readonly IJobService jobService;
        private readonly IIndustrieService industries;
        

        public CompanyController(IMapper mapper, ICompanyService companyService,
            IJobService jobService, IIndustrieService industrieService)
        {
            this.mapper = mapper;
            this.companyService = companyService;
            this.jobService = jobService;
            industries = industrieService;
        
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid company ID.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                string userId = User.GetId();
                await companyService.DeleteAsync(id, userId);
                TempData["SuccessMessage"] = "Company deleted successfully.";
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the company.";
                // Optionally log the exception: Logger.LogError(ex);
                
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var company = await companyService.GetCompanyByIdAsync(id);
                if (company.IdentityUserId != User.GetId())
                {
                    TempData["ErrorMessage"] = "You are not authorized to edit this company.";
                    return RedirectToAction("Index", "Home");
                }

                var companyFormDTO = mapper.Map<CompanyFormDTO>(company);
                await PopulateDropdowns(companyFormDTO);
                return View(companyFormDTO);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching company details.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyFormDTO companyFormDTO)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(companyFormDTO);
                return View(companyFormDTO);
            }

            try
            {
                await companyService.EditAsync(companyFormDTO);
                TempData["SuccessMessage"] = "Company updated successfully.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the company.";
                return View(companyFormDTO);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllJobsAsync(int id)
        {


            int comapnyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
            var jobs = await this.jobService.GetAllJobsForCompanyAsync(id);
            if (jobs == null)
            {
                return NotFound($"No jobs found for ID .");
                
            }
            var jobsDto = mapper.Map<List<JobFormDto>>(jobs);

            return View(jobsDto);
        }


            private async Task PopulateDropdowns(CompanyFormDTO dto)
        {
            dto.Industries = await industries.GetAllAsync();
        }
        //To Do add getAllJobs
    }
}
