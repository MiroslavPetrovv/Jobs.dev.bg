using AutoMapper;
using JobApplications.Areas.Company.Controllers.APIs;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace JobApplication.Tests.Controllers.API
{
    public class ApplicationControllerAPITests
    {
        private InMemoryDbContext dbContext;
        private ApplicationDbContext mockDatabaseContext; // Use this instead of the real database
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            // Initialize the in-memory database context
            dbContext = new InMemoryDbContext();
            mockDatabaseContext = dbContext.CreateContext(); // Use mock in-memory database

            // Seed the in-memory database
            SeedDatabase();

            // Set up AutoMapper
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Assuming a MappingProfile is defined
            });
            mapper = mappingConfig.CreateMapper();
        }
        
        [Test]
        public async Task UpdateStatusShouldReturnOk()
        {
            // Arrange
            IStatusService statusService = new StatusService(mockDatabaseContext);
            IApplicationService applicationService = new ApplicationService(mockDatabaseContext, mapper, statusService);
            var applicationController = new ApplicationControllerAPI(applicationService);
            var validStatusDto = new UpdateStatusDto()
            {
                ApplicationId = 1,
                StatusId = 2,
                StatusName = "Approved" // Corrected spelling
            };

            // Act
            var result =  applicationController.UpdateStatus(validStatusDto); // Await the result

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>());
        }
        [Test]
        public async Task UpdateStatusShouldNotReturnOk_WhenStatusDtoIsInvalid()
        {
            // Arrange
            IStatusService statusService = new StatusService(mockDatabaseContext);
            IApplicationService applicationService = new ApplicationService(mockDatabaseContext, mapper, statusService);
            var applicationController = new ApplicationControllerAPI(applicationService);
            var invalidStatusDto = new UpdateStatusDto(); // Invalid DTO

            // Act
            var result = await applicationController.UpdateStatus(invalidStatusDto);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>(), "The result should be a BadRequest response.");
        }

        [Test]
        public async Task UpdateStatusShouldReturnOk_WhenStatusDtoIsValid()
        {
            // Arrange
            IStatusService statusService = new StatusService(mockDatabaseContext);
            IApplicationService applicationService = new ApplicationService(mockDatabaseContext, mapper, statusService);
            var applicationController = new ApplicationControllerAPI(applicationService);

            var validStatusDto = new UpdateStatusDto
            {
                ApplicationId = 1, // Assuming this ID exists in the seeded data
                StatusId = 3,
                StatusName = "Aproved"
            };

            // Act
            var result = await applicationController.UpdateStatus(validStatusDto);

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>(), "The result should be an Ok response.");
        }





        private void SeedDatabase()
        {
            var company = new Company
            {
                Id = 1,
                CompanyName = "Tech Innovations Inc.",
                IndustryId = 1,
                NumbersOfEmployes = 50,
                DateOfCreation = "2010-05-01",
                IdentityUserId = Guid.NewGuid().ToString(),
                PostedJobs = new List<Job>()
            };


            var job = new Job
            {
                Id = 1,
                Title = "Software Developer",
                Salary = 60000,
                CompanyId = company.Id,
                Description = "Develop and maintain software applications.",
                JobTitleDescription = "Full-time position for a software developer.",
                IsAvaliable = true,
                WorkingHours = 40,
                PostedDate = DateTime.Now,
                Banner = "https://example.com/logo.png",
                Applications = new List<Application>()
            };


            var application = new Application
            {
                Id = 1,
                JobId = job.Id,
                IdentityUserId = Guid.NewGuid().ToString(),
                ApplyedDate = DateTime.Now,
                StatusId = 1,
                CvFilePath = new byte[] { },
                
            };

            var Status = new Status
            {
                ApplicationStatus = "Aproved",
                Id = 3
            };


            mockDatabaseContext.Companies.Add(company);
            mockDatabaseContext.Jobs.Add(job);
            mockDatabaseContext.Applications.Add(application);

        }
    }
}
