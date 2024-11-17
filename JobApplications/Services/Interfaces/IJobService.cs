using JobApplications.Data.Models;
using JobApplications.DTOs;


namespace JobApplications.Services.Interfaces

{
    public interface IJobService
    {
        Task Add(JobFormDto job);

        Task Edit(JobFormDto job);

        Task Delete(int id,string userId);

        Task<List<Job>> GetAllAsync();

        Task<Job> GetJobByIdAsync(int id);
    
        


    }
}
