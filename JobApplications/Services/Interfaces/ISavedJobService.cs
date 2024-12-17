using JobApplications.Data.Models;
using JobApplications.DTOs;

namespace JobApplications.Services.Interfaces
{
    public interface ISavedJobService
    {
        Task SaveJobAsync(string userId, int jobId);
        Task<List<JobFormDto>> GetSavedJobsAsync(string userId);

        Task RemoveSavedJobAsync(string userId, int jobId);
    }
}
