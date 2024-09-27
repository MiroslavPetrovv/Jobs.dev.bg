using JobApplications.Data;
using JobApplications.Data.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
