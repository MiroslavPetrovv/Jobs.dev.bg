using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            //make a minimal wage variable
            if (string.IsNullOrEmpty(job.Title) && job.Salary < 1000)
            {
                throw new ArgumentException("Invalid data");
            }
            Job data = mapper.Map<Job>(job);
            data.IsAvaliable = true;
            await this.dbContext.AddAsync(data);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Job jobToDelete =
                await dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobToDelete != null)
            {
                throw new InvalidOperationException("Job not finded");
            }
            else
            {
                dbContext.Jobs.Remove(jobToDelete);
                await this.dbContext.SaveChangesAsync();
            }
            
        }

        public Task Edit(JobFormDto job)
        {
            
        }
    }
}
