using JobApplications.Areas.HR.Controllers;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.Company.Controllers
{
    public class ApplicationController : BaseController
    {
        private readonly IApplicationService applicationService;
        private readonly IStatusService statusService;
        private readonly IHostEnvironment _hostEnvironment;
        public ApplicationController(IApplicationService applicationService, IStatusService statusService , IHostEnvironment hostEnvironment)
        {
            this.applicationService = applicationService;
            this.statusService = statusService;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateApplicationStatusAsync(int id,string statusName,int statusId,int jobId)
        {
            await applicationService.UpdateApplicationStatusAsync(id, statusName,statusId);

            return RedirectToAction("GetAllApplicationsForAJob", new {id=jobId});
        }
        [HttpGet]
        public async Task<IActionResult> GetAllApplicationsForAJobAsync(int id)//you give job id here
        {
            var applications = await applicationService.SeeAllApplicationsForAJobAsync(id);

            if (applications == null || !applications.Any())
            {
                return NotFound("No applications found for this job.");
            }

            return View(applications);
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetCv(string relativePath)
        {
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, relativePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var contentType = "application/pdf";
            return File(fileBytes, contentType, Path.GetFileName(fullPath));
        }

        [HttpGet("DownloadCv/{applicationId}")]
        [Authorize]
        public async Task<IActionResult> DownloadCv(int applicationId)
        {
            var application = await this.applicationService.GetApplicationByIdForDownloadingCv(applicationId);

            if (application == null || application.CvFileData == null)
            {
                return NotFound("CV not found for the given application.");
            }
            
            
            byte[] fileData = application.CvFileData; // You might need to read it from a file if it's a path
            return File(fileData, "application/pdf", $"CV_{applicationId}.pdf");
        }
    }
}
