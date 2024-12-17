using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.Admin.Controllers
{
    public class CompanyController :BaseAdminController
    {
        private readonly ICompanyService companyService;
        private readonly IJobService jobService;
        
        

        public CompanyController(ICompanyService companyService, IJobService jobservice)
        {
            this.companyService = companyService;
            this.jobService = jobservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var companies = await companyService.GetAllCompaniesAsync();
            return View(companies); 
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.GetId();
            try
            {
                
                
                await jobService.DeleteAllJobsForACompany(id);
                await companyService.DeleteAsync(id,userId);
                
                return RedirectToAction("GetAll"); // Redirect to the index page after removal
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index"); // Redirect back to the index page with an error
            }
        }
    }
}
