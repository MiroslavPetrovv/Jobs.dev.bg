using JobApplications.DTOs;
using Microsoft.AspNetCore.Identity;

namespace JobApplications.Services.Interfaces
{
    public interface IApplicationService
    {
        Task ApplyForAJobAsync(ApplicationFormDto applicationDto, string userId);
    }
}
