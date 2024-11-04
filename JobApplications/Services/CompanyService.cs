using AutoMapper;
using JobApplications.Data;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CompanyService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> GetByUserID(string userId)
        {
            var company = await this.dbContext.Companies.FirstOrDefaultAsync(c => c.IdentityUserId == userId);
            return company == null ? 0 : company.Id;
        }
    }
}
