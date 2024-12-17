using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using System.Data.Entity;


namespace JobApplications.Services
{
    public class SavedJobService : ISavedJobService
    {
        private readonly ApplicationDbContext dbContext;

        public SavedJobService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    public async Task SaveJobAsync(string userId, int jobId)
    {
            var existingSavedJob = await dbContext.SavedJobs.FirstOrDefaultAsync(sj=>sj.JobId==jobId && sj.IdentityUserId ==userId);
           

            if (existingSavedJob != null)
            {
                return;
            }
            var newSavedJob = new SavedJob
            {
                IdentityUserId = userId,
                JobId = jobId
            };
            await dbContext.SavedJobs.AddAsync(newSavedJob);
            await dbContext.SaveChangesAsync();
        }

    public async Task<List<JobFormDto>> GetSavedJobsAsync(string userId)
    {
            var jobFormDtos = await dbContext.SavedJobs
            .AsNoTracking() 
            .Where(sj => sj.IdentityUserId == userId) 
            .Include(sj => sj.Job) 
            .Select(sj => new JobFormDto
            {
                Id = sj.Job.Id,
                Title = sj.Job.Title,
                Salary = sj.Job.Salary,
                CompanyId = sj.Job.CompanyId,
                CompanyName = sj.Job.Company.CompanyName,
                Description = sj.Job.Description,
                JobTitleDescription = sj.Job.JobTitleDescription,
                WorkingHours = sj.Job.WorkingHours,
                IsAvaliable = sj.Job.IsAvaliable,
                Banner = sj.Job.Banner,
                PostedDate = sj.Job.PostedDate
            })
            .ToListAsync();

            return jobFormDtos;
        }

    public async Task RemoveSavedJobAsync(string userId, int jobId)
    {
        var savedJob = await dbContext.SavedJobs
            .FirstOrDefaultAsync(sj => sj.IdentityUserId == userId && sj.JobId == jobId);

        if (savedJob != null)
        {
            dbContext.SavedJobs.Remove(savedJob);
            await dbContext.SaveChangesAsync();
        }
    }
    }
}
