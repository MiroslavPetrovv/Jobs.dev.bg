using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobApplications.Controllers
{

    public class CompanyController : Controller
    {
        // REMOVE DATABASE - ADD SERVICES

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




        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (string.IsNullOrEmpty(User.GetId()))
            {
                //to return error
                TempData["ErrorNotAuth"] = "You should log in in your profile first!";

                return RedirectToAction("Index", "Home"); // Login
            }

            var companyDto = new CompanyFormDTO
            {
                IdentityUserId = User.GetId()
            };


            await FilledDropdowns(companyDto);

            return View(companyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyFormDTO companyDto)
        {


            if (!ModelState.IsValid)
            {
                await FilledDropdowns(companyDto);
                return View();

            }
            if (string.IsNullOrEmpty(User.GetId()))
            {
                TempData["UserLost"] = "Login again";
            }
            await companyService.AddAsync(companyDto);



            return RedirectToAction("GetAll");
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
            var comapnyId =await companyService.GetCompanyIdByUserIdAsync(userId);
            var comapny = await companyService.GetCompanyByIdAsync(comapnyId);
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
            Company? company = await this.companyService.GetCompanyByIdAsync(id);
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
        public async Task<IActionResult> GetAllJobs()
        {
            var comapnyId = await companyService.GetCompanyIdByUserIdAsync(User.GetId());
            var jobs = await this.jobService.GetAllJobsForCompanyAsync(comapnyId);
            if (jobs == null)
            {
                return NotFound($"No jobs found for ID .");
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
