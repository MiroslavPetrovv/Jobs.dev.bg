using JobApplications.Data.Models;
using JobApplications.DTOs;

namespace JobApplications.Services.Interfaces
{
    public interface ISavedJobService
    {
        Task SaveJobAsync(string userId, int jobId);
        List<JobFormDto> GetSavedJobs(string userId);

        Task RemoveSavedJobAsync(string userId, int jobId);

        Task RemoveAllSavedJobsForAJob(int jobId);
    }
}
