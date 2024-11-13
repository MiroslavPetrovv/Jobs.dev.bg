
using JobApplications.DTOs;
namespace JobApplications.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<int> GetByUserID(string userId);

        Task Add(CompanyFormDTO job);

        Task Edit(CompanyFormDTO CompanyEditViewModel);

        Task Delete(int id);

        Task<List<JobFormDto>> GetAllJobs(int companyId);
    }
}
