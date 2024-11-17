using JobApplications.Data.Models;

namespace JobApplications.Services.Interfaces
{
    public interface IIndustrieService
    {
        public Task<List<Industry>> GetAllAsync();
        public List<Industry> GetAll();
    }
}
