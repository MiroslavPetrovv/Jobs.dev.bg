using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;

namespace JobApplications.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext  dbContext;
        private readonly IMapper mapper;

        public JobService(ApplicationDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Add(JobFormDto job)
        {
            if (string.IsNullOrEmpty(job.Title) && job.Salary < 1000)
            {
                throw new ArgumentException("Invalid data");
            }
            Job data = mapper.Map<Job>(job);
            data.IsAvaliable = true;
            await this.dbContext.AddAsync(data);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
