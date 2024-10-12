using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobApplications.Controllers
{
    public class CompanyController : Controller
    {
        private ApplicationDbContext data;
        private IMapper mapper;

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
            }   

            CompanyFormDTO companyDto = new CompanyFormDTO();

            FilledDropdowns(companyDto);

            return View(companyDto);
        }

        [HttpPost]
        public IActionResult Add(CompanyFormDTO companyDto)
        {

            if (string.IsNullOrEmpty(User.GetId()))
            {
                //to return error
            }   

            // fk to identity user 

            companyDto.IdentityUserId = User.GetId();
            var company = mapper.Map<Company>(companyDto);
            data.Add(company);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }

        public IActionResult Delete(int id)
        {
            Company company = data.Companies.FirstOrDefault(x => x.Id == id);
            data.Companies.Remove(company);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Company company = data.Companies.FirstOrDefault(x => x.Id == id);


            return View(company);
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            //Add if statements

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
