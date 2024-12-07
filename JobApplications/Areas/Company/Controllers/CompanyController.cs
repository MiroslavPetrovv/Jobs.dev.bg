using AutoMapper;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.HR.Controllers
{
    public class CompanyController : BaseController
    {
        private IMapper mapper;
        private readonly ICompanyService companyService;
        private readonly IJobService jobService;
        private readonly IIndustrieService industries;

        public CompanyController(IMapper mappingProfile, ICompanyService companyService,
            IJobService jobService, IIndustrieService industrieService)
        {

            mapper = mappingProfile;
            this.companyService = companyService;
            this.jobService = jobService;
            this.industries = industrieService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                //TempData[""]
                return RedirectToAction("Index", "Home");
            }
            string userId = User.GetId();
            var comapny = companyService.GetCompanyIdByUserIdAsync(userId);
            if (comapny == null)
            {
                TempData["ErrorNotAuth"] = "You are not authorized";
                return RedirectToAction("Index", "Home");
            }

            await companyService.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await this.companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (company.IdentityUserId != User.GetId())
            {
                TempData["ErrorNotAuth"] = "You are not authorized";
            }

            CompanyFormDTO companyFormDTO = new CompanyFormDTO();
            companyFormDTO = mapper.Map<CompanyFormDTO>(company);
            await FilledDropdowns(companyFormDTO);
            return View(companyFormDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CompanyFormDTO companyFormDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await companyService.EditAsync(companyFormDTO);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllJobsAsync(int id)
        {
            int comapnyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
            var jobs = await this.jobService.GetAllJobsForCompanyAsync(id);
            if (jobs == null)
            {
                //To Do temp data
                return NotFound($"No jobs found for ID.");
            }

            return View(jobs);
        }

        private async Task FilledDropdowns(CompanyFormDTO dto)
        {
            List<Industry> industriesList = await industries.GetAllAsync();

            dto.Industries = industriesList;
        }
    }
}
