using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobApplications.Controllers
{
    public class CompanyController : Controller
    {
        // REMOVE DATABASE - ADD SERVICES
        private  ApplicationDbContext data;
        private  IMapper mapper;
        private readonly ICompanyService companyService;

        public CompanyController(ApplicationDbContext context,IMapper mappingProfile)
        {
            data = context;
            mapper = mappingProfile;
        }
        

        public Company Get()
        {
            return this.data.Companies.FirstOrDefault();
        }

        [HttpGet]
        public IActionResult Add()
        {            
            if (string.IsNullOrEmpty(User.GetId()))
            {
                //to return error
                TempData["ErrorNotAuth"] = "You should log in in your profile first!";

                return RedirectToAction( "Index" ,"Home"); // Login
            }   

            CompanyFormDTO companyDto = new CompanyFormDTO();

            FilledDropdowns(companyDto);

            return View(companyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyFormDTO companyDto)
        {
            // ADD MULTIPLE VALIDATIONS FOR THE DATABASE INSERT
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrEmpty(User.GetId()))
            {
                TempData["UserLost"] = "Login again";
            }
            await companyService.Add(companyDto, User.GetId());
              
            
           
            return RedirectToAction("GetAll");
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Company company = data.Companies.FirstOrDefault(x => x.Id == id);
            data.Companies.Remove(company);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            // DTO
            Company company = data.Companies.FirstOrDefault(x => x.Id == id);


            return View(company);
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            //Add if statements
// VALIDATIONS + DTO 
            data.Companies.Update(company);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }

        private void FilledDropdowns(CompanyFormDTO dto)
        {
            List<Industry> industries = data.Industries.ToList();

            dto.Industries = industries;
        }
    }
}
