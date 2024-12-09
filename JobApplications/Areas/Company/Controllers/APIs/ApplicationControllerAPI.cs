using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.Company.Controllers.APIs
{
    [Route("company/api/application")]
    [ApiController]
    public class ApplicationControllerAPI : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationControllerAPI(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto status){

            await applicationService.UpdateApplicationStatusAsync(status.ApplicationId, status.StatusName, status.StatusId);
           
            return Ok();
        }

    }
}
