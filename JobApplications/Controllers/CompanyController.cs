using JobApplications.Data;
using JobApplications.Data.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobApplications.Controllers
{
    public class CompanyController : Controller
    {
        private ApplicationDbContext data;

        public CompanyController(ApplicationDbContext context)
        {
            data = context;
        }

        public Company Get()
        {
            return this.data.Companies.FirstOrDefault();
        }
        [HttpGet]
        public IActionResult Add()
        {
            Company company = new Company();
            return View(company);
        }

        [HttpPost]
        public IActionResult Add(Company company)
        {
            
            //make a drop down menu for the company
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
    }
}
