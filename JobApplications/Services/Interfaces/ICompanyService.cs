using JobApplications.DTOs.ViewModel;
using JobApplications.DTOs;
using JobApplications.DTOs.ViewModel.CompanyViewModels;
namespace JobApplications.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<int> GetByUserID(string userId);

        Task Add(CompanyFormDTO job);

        Task Edit(CompanyEditViewModel CompanyEditViewModel);

        Task Delete(int id);
    }
}
