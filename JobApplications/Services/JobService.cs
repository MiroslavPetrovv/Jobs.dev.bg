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
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ICompanyService companyService;

        public JobService(ApplicationDbContext dbContext, IMapper mapper, ICompanyService companyService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.companyService = companyService;
        }

        public async Task<List<Job>> GetAllAsync()
        {
            return await this.dbContext.Jobs.Include(x => x.Company).ToListAsync();
        }

        public async Task Add(JobFormDto job)
        {

            ValidateJob(job);

            Job data = mapper.Map<Job>(job);
            data.IsAvaliable = true;
            await dbContext.AddAsync(data);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id, string userId)
        {

            ValidateUser(userId);

            Job? jobToDelete = await dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobToDelete == null)
            {
                throw new InvalidOperationException("Job not found");
            }

            var compId = await companyService.GetByUserID(userId);
            if (compId == 0)
            {
                throw new InvalidOperationException("Company associated with the user not found");
            }

            dbContext.Jobs.Remove(jobToDelete);
            await dbContext.SaveChangesAsync();


        }

        public async Task Edit(JobFormDto job)
        {
            ValidateJob(job);

            Job? editedJob = await dbContext.Jobs.FindAsync(job.Id);
            if (editedJob == null)
            {
                throw new InvalidOperationException("Job offer not found");
            }

            editedJob.Id = job.Id;
            editedJob.CompanyId = job.CompanyId;
            editedJob.Description = job.Description;
            editedJob.Title = job.Title;
            editedJob.WorkingHours = job.WorkingHours;
            editedJob.Salary = job.Salary;
            editedJob.IsAvaliable = job.IsAvaliable;
            editedJob.Banner = job.Banner;

            this.dbContext.Update(editedJob);
            await this.dbContext.SaveChangesAsync();

        }

        public async Task<List<Job>> GetAllForCompany(int companyId)
        {
            return await this.dbContext.Jobs.Where(x => x.CompanyId == companyId).ToListAsync();
        }
        public void ValidateJob(JobFormDto job)
        {
            const decimal MinimalWage = 1000;

            if (string.IsNullOrEmpty(job.Title) || job.Title.Length >= 100)
            {
                throw new ArgumentException("Invalid Title: Title cannot be null or exceed 100 characters");
            }

            if (job.Salary < MinimalWage)
            {
                throw new ArgumentException("Salary cannot be below the minimal wage");
            }

            if (string.IsNullOrEmpty(job.Description))
            {
                throw new ArgumentException("Description cannot be empty");
            }

            if (job.WorkingHours <= 0)
            {
                throw new ArgumentException("Working hours must be greater than zero");
            }

            if (job.CompanyId < 1)
            {
                throw new ArgumentException("Invalid company");
            }
        }
        private void ValidateUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID is required");
            }
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid Job Id");
            }
            var job =await dbContext.Jobs.FindAsync(id);
            if (job == null)
            {
                throw new ArgumentException("No existing Job");
            }
            return job;
        }
    }
}
