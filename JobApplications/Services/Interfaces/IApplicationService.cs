using Microsoft.AspNetCore.Identity;

namespace JobApplications.Services.Interfaces
{
    public interface IApplicationService
    {
        Task ApplyForAJob(int jobid,IdentityUser applicant);
    }
}
