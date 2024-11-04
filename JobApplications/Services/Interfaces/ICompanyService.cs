namespace JobApplications.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<int> GetByUserID(string userId);
    }
}
