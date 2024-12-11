using JobApplications.Data.Models;
using JobApplications.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace JobApplications.Services.Interfaces

{
    public interface IJobService
    {
        Task AddAsync(JobFormDto job);

        Task EditAsync(JobFormDto job);

        Task Delete(int id,string userId);

        Task<List<Job>> GetAllAsync();

        Task<List<JobFormDto>> GetAllAvailableJobs();

        Task<Job> GetJobByIdAsync(int id);

        Task<List<Application>> SeeAllApplicants(int id);

        Task<List<JobFormDto>> GetAllJobsForCompanyAsync(int companyId);

        Task<List<Job>> FilterJobsAsync(JobFilterParams filterParams);

        IEnumerable<Job> FilterJobs(List<Job> jobs, JobFilterParams filterParams);





    }
}
