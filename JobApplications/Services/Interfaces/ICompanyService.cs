
using JobApplications.Data.Models;
using JobApplications.DTOs;
namespace JobApplications.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<int> GetCompanyIdByUserIdAsync(string userId);

        Task AddAsync(CompanyFormDTO job);

        Task EditAsync(CompanyFormDTO CompanyEditViewModel);

        Task DeleteAsync(int id,string userId);

        Task<Company> GetCompanyByIdAsync(int id);
    }
}
