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
            var existingSavedJob = dbContext.SavedJobs.FirstOrDefault(sj => sj.JobId == jobId && sj.IdentityUserId == userId);


            if (existingSavedJob != null)
            {
                throw new InvalidOperationException("Job already saved for this user");
            }
            var newSavedJob = new SavedJob
            {
                IdentityUserId = userId,
                JobId = jobId
            };
            await dbContext.SavedJobs.AddAsync(newSavedJob);
            await dbContext.SaveChangesAsync();
        }

        public List<JobFormDto> GetSavedJobs(string userId)
        {
            var jobFormDtos = dbContext.SavedJobs
            .Include(sj => sj.Job)
            .Where(sj => sj.IdentityUserId == userId)
            .Select(sj => new JobFormDto
            {
                Id = sj.Job.Id,
                Title = sj.Job.Title,
                Salary = sj.Job.Salary,
                CompanyId = sj.Job.CompanyId,
                //CompanyName = sj.Job.Company.CompanyName,
                Description = sj.Job.Description,
                JobTitleDescription = sj.Job.JobTitleDescription,
                WorkingHours = sj.Job.WorkingHours,
                IsAvaliable = sj.Job.IsAvaliable,
                Banner = sj.Job.Banner,
                PostedDate = sj.Job.PostedDate
            })
            .ToList();

            return jobFormDtos;
        }

        public async Task RemoveSavedJobAsync(string userId, int jobId)
        {
            var savedJob = dbContext.SavedJobs
                .FirstOrDefault(sj => sj.IdentityUserId == userId && sj.JobId == jobId);

            if (savedJob != null)
            {
                dbContext.SavedJobs.Remove(savedJob);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAllSavedJobsForAJob(int jobId)
        {
            var savedJobs = dbContext.SavedJobs
                 .FirstOrDefault(sj => sj.JobId == jobId);

            if(savedJobs!= null)
            {
                dbContext.RemoveRange(savedJobs);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                return;
            }
        }
    }
}
