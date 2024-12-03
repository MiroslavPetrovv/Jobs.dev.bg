using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop.Implementation;

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
        public async Task ApplyForAJobAsync(ApplicationFormDto applicationDto)
        {
            var jobForApplying = await this.dbContext.Jobs.FindAsync(applicationDto.JobId);
            if (jobForApplying == null)
            {
                throw new ArgumentException("Invalid Job for applying");
            }

            Application application = new Application
            {
                Id = applicationDto.Id,
                JobId = jobForApplying.Id,
                ApplyedDate = DateTime.Now,
                IdentityUserId = applicationDto.IdentityUserId,
                StatusId = 1
            };
            var job = await dbContext.Jobs.FindAsync(application.JobId);
            if(job == null)
            {
                throw new ArgumentException("Invalid JobId");
            }
            job.Applications.Add(application);
            await this.dbContext.Applications.AddAsync(application);
            await this.dbContext.SaveChangesAsync();

            
        }
    }
}
