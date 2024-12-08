﻿using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            return await this.dbContext.Jobs.Include(x => x.Company).Where(x => x.IsAvaliable == true).ToListAsync();
        }

        public async Task AddAsync(JobFormDto job)
        {

            ValidateJob(job);

            Job data = mapper.Map<Job>(job);
            data.PostedDate = DateTime.Now.Date;
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

            var compId = await companyService.GetCompanyIdByUserIdAsync(userId);
            if (compId == 0)
            {
                throw new InvalidOperationException("Company associated with the user not found");
            }

            dbContext.Jobs.Remove(jobToDelete);
            await dbContext.SaveChangesAsync();


        }

        public async Task EditAsync(JobFormDto job)
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

        public async Task<List<JobFormDto>> GetAllJobsForCompanyAsync(int companyId)
        {
            if (companyId == 0)
            {
                throw new ArgumentException("There is no company mathcing this id");
            }
            var companyJobs = await this.dbContext.Jobs
                    .Include(j => j.Company)
                    .Where(x => x.CompanyId == companyId)
                    .Select(x => new JobFormDto
                    {
                        Id = x.Id,
                        CompanyId = companyId,
                        Description = x.Description,
                        Title = x.Title,
                        WorkingHours = x.WorkingHours,
                        Salary = x.Salary,
                        IsAvaliable = x.IsAvaliable,
                        Banner = x.Banner,
                        JobTitleDescription = x.JobTitleDescription,

                    })
                    .ToListAsync();
            return companyJobs;
        }


        public async Task<Job> GetJobByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid Job Id");
            }
            var job = await dbContext.Jobs.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == id);
            if (job == null)
            {
                throw new ArgumentException("No existing Job");
            }
            return job;
        }

        public async Task<List<Application>> SeeAllApplicants(int id)
        {
            return await this.dbContext.Applications
                .Include(x => x.IdentityUser)
                .Include(x => x.Job)
                .Include(x => x.Status)
                .Where(x => x.JobId == id)
                .ToListAsync();
        }

        public async Task<List<JobFormDto>> GetAllAvailableJobs()
        {
            var jobs = await this.dbContext.Jobs
                .AsNoTracking()
                .Include(j => j.Company)
                .Where(j => j.IsAvaliable == true)
                .Select(j => new JobFormDto
                {
                    Id = j.Id,
                    IsAvaliable = j.IsAvaliable,
                    Banner = j.Banner,
                    CompanyId = j.CompanyId,
                    CompanyName = j.Company.CompanyName, // Map the Company name
                    Description = j.Description,
                    JobTitleDescription = j.JobTitleDescription,
                    Salary = j.Salary,
                    Title = j.Title,
                    WorkingHours = j.WorkingHours,
                    PostedDate = j.PostedDate,
                })
                .OrderBy(j => j.PostedDate)
                .ToListAsync();

            return jobs;
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
    }
}
