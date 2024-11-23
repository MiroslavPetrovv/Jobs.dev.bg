using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IApplicationService applicationService;


        public ApplicationController(ICompanyService companyService, IJobService jobService, IApplicationService applicationService)
        {

            this.jobService = jobService;
            this.companyService = companyService;
            this.applicationService = applicationService;
        }
        [HttpGet]
        public IActionResult ApplyForJob(int id)
        {
            
            if (string.IsNullOrEmpty(User.GetId()))
            {
                //to return error
                TempData["ErrorNotAuth"] = "You should log in in your profile first!";

                return RedirectToAction("Index", "Home"); // Login
            }

            var applicationDto = new ApplicationFormDto
            {
                IdentityUserId = User.GetId(),
                JobId = id
            };

            return View(applicationDto);
        }




    }
}
