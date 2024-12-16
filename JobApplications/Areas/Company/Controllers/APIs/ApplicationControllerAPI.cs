using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

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
            if (status == null || string.IsNullOrEmpty(status.StatusName)|| status.StatusId==0||status.ApplicationId==0)
            {
                return BadRequest();
            }
            
            await applicationService.UpdateApplicationStatusAsync(status.ApplicationId, status.StatusName, status.StatusId);
           
            return Ok();
        }

    }
}
