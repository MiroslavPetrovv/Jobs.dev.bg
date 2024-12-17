using JobApplications.Controllers;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.Admin.Controllers
{
    public class ApplicationController : BaseAdminController
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IApplicationService applicationService;
      


        public ApplicationController(ICompanyService companyService, IJobService jobService,
            IApplicationService applicationService)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
            
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var applications = await applicationService.SeeAllApplications();
                return View(applications);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(500, "Internal server error: " + ex.Message); // Return a 500 Internal Server Error response
            }
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                bool result = await applicationService.RemoveApplicationAsync(id);
                if (!result)
                {
                    ModelState.AddModelError("", "Application not found or could not be removed.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while removing the application: " + ex.Message);
            }

            return RedirectToAction("GetAll");
        }

    }
}
