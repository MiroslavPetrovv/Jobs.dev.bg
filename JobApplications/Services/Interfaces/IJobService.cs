using JobApplications.DTOs;
using JobApplications.DTOs.ViewModel.JobViewModels;

namespace JobApplications.Services.Interfaces

{
    public interface IJobService
    {
        Task Add(JobFormDto job);

        Task Edit(JobFormDto job);

        Task Delete(int id,string userId);


        
    }
}
