using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JobApplications.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ApplicationService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task ApplyForAJob(int jobid, IdentityUser applicant)
        {
            var jobForApplying = await this.dbContext.Jobs.FindAsync(jobid);
            if (jobForApplying == null)
            {
                throw new ArgumentException("Invalid Job for applying");
            }

            Application application = new Application
            {

            };

            
        }
    }
}
