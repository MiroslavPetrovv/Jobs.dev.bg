using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace JobApplications.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IStatusService statusService;

        public ApplicationService(ApplicationDbContext dbContext, IMapper mapper, IStatusService statusService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.statusService = statusService;
        }

        public async Task ApplyForAJobAsync(ApplicationFormDto applicationDto, string userId, List<IFormFile> CvFile)
        {
            byte[] cv = new byte[8000];
            foreach (var item in CvFile)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        cv = stream.ToArray();
                    }
                }
            }

            if (CvFile.Count <= 0)
            {
                throw new InvalidOperationException("Cant apply for a job without Cv");
            }

            var jobForApplying = await dbContext.Jobs
                .FirstOrDefaultAsync(j => j.Id == applicationDto.JobId);

            if (jobForApplying == null)
            {
                throw new ArgumentException("The job you are trying to apply for does not exist.");
            }


            var application = new Application
            {
                JobId = jobForApplying.Id,
                ApplyedDate = DateTime.UtcNow,
                IdentityUserId = userId,
                StatusId = 1, //Pending
                CvFilePath = cv
            };


            await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ApplicationDownloadDto> GetApplicationByIdForDownloadingCv(int id)
        {
            var applicationForDownload = await this.dbContext.Applications
                .FindAsync(id);
            if (applicationForDownload == null)
            {
                throw new ArgumentException("There is no application with this id");
            }
            var applicationDto = new ApplicationDownloadDto
            {
                ApplicationId = applicationForDownload.Id,
                CvFileData = applicationForDownload.CvFilePath
            };
            return applicationDto;
        }



        public async Task<List<ApplicationDisplayingFormDto>> SeeAllApplications()
        {
            var applications = await dbContext.Applications
            .Include(a => a.Job) // Include the Job entity
            .Include(a => a.Status) // Include the Status entity
            .Include(a => a.IdentityUser) // Include the User entity
            .Select(a => new ApplicationDisplayingFormDto
            {
                Id = a.Id,
                JobId = a.JobId,
                JobTitle = a.Job.Title, // Assuming Job has a Title property
                IdentityUserId = a.IdentityUserId,
                UserName = a.IdentityUser.UserName,
                ApplyedDate = a.ApplyedDate,
                StatusId = a.StatusId,
                StatusName = a.Status.ApplicationStatus,
            })
            .OrderBy(a => a.JobTitle) // Order by Job Title
            .ToListAsync();

            return applications;
        }

        public async Task<List<ApplicationDisplayingFormDto>> SeeAllApplicationsForAJobAsync(int jobId)
        {
            var applications = await dbContext.Applications
                .Include(a => a.Job)
                .Include(a => a.Status)
                .Include(a => a.IdentityUser)
                .Where(a => a.JobId == jobId)
                .Select(a => new ApplicationDisplayingFormDto
                {
                    Id = a.Id,
                    JobId = a.JobId,
                    JobTitle = a.Job.Title,
                    IdentityUserId = a.IdentityUserId,
                    UserName = a.IdentityUser.UserName,
                    ApplyedDate = a.ApplyedDate,
                    StatusId = a.StatusId,
                    StatusName = a.Status.ApplicationStatus,
                })
                .OrderBy(x => x.ApplyedDate)
                .ToListAsync();

            return applications;
        }
        public async Task<bool> UpdateApplicationStatusAsync(int applicationId, string statusName, int statusId)
        {
            return await statusService.UpdateApplicationStatusAsync(applicationId, statusName, statusId);
        }

        public async Task<List<ApplicationDisplayingFormDto>> SeeAllApplicationByUserId(string userId)
        {
            var applications = await this.dbContext.Applications
                .AsNoTracking()
                .Include(a => a.Job)
                .Include(a => a.IdentityUser)
                .Include(a => a.Status)
                .Where(a => a.IdentityUserId == userId)
                .Select(a => new ApplicationDisplayingFormDto
                {
                    Id = a.Id,
                    ApplyedDate = a.ApplyedDate,
                    StatusId = a.StatusId,
                    StatusName = a.Status.ApplicationStatus,
                    IdentityUserId = a.IdentityUserId,
                    JobTitle = a.Job.Title,
                    JobId = a.Job.Id,
                    UserName = a.IdentityUser.UserName,
                })
                .OrderBy(x => x.ApplyedDate)
                .ToListAsync();

            return applications;
        }

        public async Task DeleteApplicationForAllJobsFromACompany(int companyId)
        {
            if (companyId <= 0)
            {
                throw new ArgumentException("Invalid company ID.");
            }

            // Retrieve all jobs for the given companyId
            var jobs = await dbContext.Jobs
                .Where(j => j.CompanyId == companyId)
                .ToListAsync();

            // If no jobs found, return or throw an exception
            if (!jobs.Any())
            {
                return;
            }

            // Retrieve all applications for the jobs
            var jobIds = jobs.Select(j => j.Id).ToList();
            var applicationsToDelete = await dbContext.Applications
                .Where(a => jobIds.Contains(a.JobId))
                .ToListAsync();

            // Delete the applications
            dbContext.Applications.RemoveRange(applicationsToDelete);
            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> RemoveApplicationAsync(int applicationId)
        {
            var application = await dbContext.Applications.FindAsync(applicationId);
            if (application == null)
            {
                return false; // Application not found
            }

            dbContext.Applications.Remove(application);
            await dbContext.SaveChangesAsync();
            return true; // Successfully removed
        }
    }
}
