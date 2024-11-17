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
        private ApplicationDbContext data;
        private IMapper mapper;
        private readonly ICompanyService companyService;
        private readonly IJobService jobService;

        public CompanyController(ApplicationDbContext context, IMapper mappingProfile, ICompanyService companyService, IJobService jobService)
        {
            data = context;
            mapper = mappingProfile;
            this.companyService = companyService;
            this.jobService = jobService;
        }


        public async Task<Company> Get()
        {
            return await this.data.Companies.FirstOrDefaultAsync();
        }

        [HttpGet]
        public IActionResult Add()
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


            FilledDropdowns(companyDto);

            return View(companyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyFormDTO companyDto)
        {


            if (!ModelState.IsValid)
            {
                FilledDropdowns(companyDto);
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
            var comapny = companyService.GetByUserID(userId);
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
            Company? company = await this.data.Companies.FirstOrDefaultAsync(x=> x.Id ==id);
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
            FilledDropdowns(companyFormDTO);
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
        public async Task<IActionResult> GetAllJobs(int id)
        {
            if (id <= 0)
            {

                return BadRequest("Invalid ID. The ID must be a positive integer.");
            }

               var jobs =await this.companyService.GetAllJobsAsync(id);
            if (jobs == null)
            {
                return NotFound($"No jobs found for ID {id}.");
            }

            return View(jobs);
        }

        private void FilledDropdowns(CompanyFormDTO dto)
        {
            List<Industry> industries = data.Industries.ToList();

            dto.Industries = industries;
        }

    }
}
