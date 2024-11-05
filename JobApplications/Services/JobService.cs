using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.DTOs.ViewModel.JobViewModels;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext  dbContext;
        private readonly IMapper mapper;
        private readonly ICompanyService companyService;

        public JobService(ApplicationDbContext dbContext,IMapper mapper,ICompanyService companyService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.companyService = companyService;
        }

        public async Task Add(JobFormDto job)
        {
            
            //make a minimal wage variable
            if (string.IsNullOrEmpty(job.Title) && job.Salary < 1000)
            {
                throw new ArgumentException("Invalid data");
            }
            if (job.CompanyId < 1)
            {
                throw new ArgumentException("Invalid company");
            }
            Job data = mapper.Map<Job>(job);
            data.IsAvaliable = true;
            await this.dbContext.AddAsync(data);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id,string userId)
        {
            
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User was lost");
            }
            Job? jobToDelete =
                await dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobToDelete == null)
            {
                throw new InvalidOperationException("Job not finded");
            }

                dbContext.Jobs.Remove(jobToDelete);
                await this.dbContext.SaveChangesAsync();
            
            
        }

        public async Task Edit(JobFormDto job)
        {

            
            
            if (job.Salary <= 1000)
            {
                throw new ArgumentException("Salary cannot be under minimal wage");
            }
            //make a data validation for string length
            if (string.IsNullOrEmpty(job.Title) || job.Title.Count() >= 100)
            {
                throw new ArgumentException("Invalid Title");
            }
            //make a limit for the description lenght
            if (!string.IsNullOrEmpty(job.Description))
            {
                throw new ArgumentException("Description cannot be empty");
            }
            if (job.WorkingHours <= 0)
            {
                throw new ArgumentException("Under minimal working hours");
            }

            Job? editedJob = await this.dbContext.Jobs.FindAsync(job.Id);
            if (editedJob == null)
            {
                throw new InvalidOperationException("Invalid job offer created");
            }

            editedJob.Id = job.Id;
            editedJob.CompanyId = job.CompanyId;
            editedJob.Description = job.Description;
            editedJob.Title = job.Title;
            editedJob.WorkingHours = job.WorkingHours;
            editedJob.Salary = job.Salary;
            editedJob.IsAvaliable = job.IsAvaliable;

            await this.dbContext.SaveChangesAsync();

        }
    }
}
