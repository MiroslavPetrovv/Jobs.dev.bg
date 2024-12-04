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

namespace JobApplications.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IHostEnvironment _hostEnvironment;

        public ApplicationService(ApplicationDbContext dbContext, IMapper mapper, IHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task ApplyForAJobAsync(ApplicationFormDto applicationDto, string userId)
        {
            // Fetch the job
            var jobForApplying = await dbContext.Jobs.FindAsync(applicationDto.JobId);
            if (jobForApplying == null)
            {
                throw new ArgumentException("The job you are trying to apply for does not exist.");
            }

           
            if (applicationDto.CvFile == null || applicationDto.CvFile.Length == 0)
            {
                throw new ArgumentException("A CV is required to apply for this job.");
            }

            if (applicationDto.CvFile.ContentType != "application/pdf")
            {
                throw new ArgumentException("Only PDF files are accepted.");
            }

            
            string cvFilePath = await UploadCvFileAsync(applicationDto.CvFile);

            
            var application = new Application
            {
                JobId = jobForApplying.Id,
                ApplyedDate = DateTime.UtcNow,
                IdentityUserId = userId,
                StatusId = 1, //Pending
                CvFilePath = cvFilePath
            };

            // Save the application
            jobForApplying.Applications.Add(application);
            await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
        }

        
        private async Task<string> UploadCvFileAsync(IFormFile cvFile)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "cv");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(cvFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await cvFile.CopyToAsync(fileStream);
            }

            return Path.Combine("/uploads/cv", fileName); // Relative path for serving the file
        }

    }
}
