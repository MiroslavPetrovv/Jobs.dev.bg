
using JobApplications.DTOs;
namespace JobApplications.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<int> GetByUserID(string userId);

        Task AddAsync(CompanyFormDTO job);

        Task EditAsync(CompanyFormDTO CompanyEditViewModel);

        Task DeleteAsync(int id);

        Task<List<JobFormDto>> GetAllJobsAsync(int companyId);
    }
}
