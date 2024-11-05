using JobApplications.DTOs;

namespace JobApplications.Services.Interfaces

{
    public interface IJobService
    {
        Task Add(JobFormDto job);

        Task Edit(int id);

        Task Delete(int id);


        
    }
}
