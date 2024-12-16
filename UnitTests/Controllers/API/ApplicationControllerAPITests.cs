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
        public void UpdateStatusShouldNotReturnOk()
        {
            //Arange
            IStatusService statusService = new StatusService(mockDatabaseContext);
            IApplicationService applicationService = new ApplicationService(mockDatabaseContext,mapper,statusService);
            var applicationController = new ApplicationControllerAPI(applicationService);
            var invalidStatusDto = new UpdateStatusDto();

            //Act
            var result =  applicationController.UpdateStatus(invalidStatusDto);

            

            //Asert
            Assert.That(result.Result, Is.InstanceOf<BadRequestResult>(), "The result should be a BadRequest response.");

        }

        public void UpdateStatusShouldReturnOk()
        {

        }

        

        

        private void SeedDatabase()
        {
            var company = new Company();
            {

            };
            var job = new Job()
            {

            };
            var statusDto = new Application()
            {

            };
           
        }
    }
}
