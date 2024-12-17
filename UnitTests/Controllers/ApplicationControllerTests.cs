using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using JobApplications.Areas.Company.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobApplication.Tests;
using JobApplications.Data.Models;
using JobApplications.Data;
using JobApplications.Extensions;
using JobApplications.Services;
using Microsoft.AspNetCore.Identity;

[TestFixture]
public class ApplicationControllerTests
{
    private InMemoryDbContext dbContext;
    private ApplicationDbContext mockDatabaseContext;
    private IMapper mapper;

    [SetUp]
    public void Setup()
    {
        // Initialize the in-memory database context
        dbContext = new InMemoryDbContext();
        mockDatabaseContext = dbContext.CreateContext(); // Use mock in-memory database

        // Set up AutoMapper
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Assuming a MappingProfile is defined
        });
        mapper = mappingConfig.CreateMapper();
    }

    [Test]
    public async Task GetAllApplicationsForAJob_ShouldReturnNotFound_WhenNoApplicationsExist()
    {
        // Arrange
        var applicationService = new ApplicationService(mockDatabaseContext, mapper, new StatusService(mockDatabaseContext));
        var controller = new ApplicationController(applicationService, new StatusService(mockDatabaseContext));

        // Act
        var result = await controller.GetAllApplicationsForAJobAsync(1); // Job ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>(), "The result should be a NotFound response.");
    }

    [Test]
    public async Task GetAllApplicationsForAJob_ShouldReturnApplications_WhenApplicationsExist()
    {
        // Arrange
        SeedDatabaseForApplications();

        var applicationService = new ApplicationService(mockDatabaseContext, mapper, new StatusService(mockDatabaseContext));
        var controller = new ApplicationController(applicationService, new StatusService(mockDatabaseContext));

        // Act
        var result = await controller.GetAllApplicationsForAJobAsync(1); // Job ID that exists

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>(), "The result should be a ViewResult."); // Assuming ApplicationDto is the model used
        
    }

    private void SeedDatabaseForApplications()
    {
        var company = new Company
        {
            Id = 1,
            CompanyName = "Tech Innovations Inc.",
            IndustryId = 1,
            NumbersOfEmployes = 50,
            DateOfCreation = "2010-05-01",
            IdentityUserId = Guid.NewGuid().ToString()
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
            Banner = "https://example.com/logo.png"
        };

        var application = new Application
        {
            Id = 1,
            JobId = job.Id,
            IdentityUserId = Guid.NewGuid().ToString(),
            ApplyedDate = DateTime.Now,
            StatusId = 1,
            CvFilePath = new byte[] { }
        };

        // Add the entities to the in-memory database
        mockDatabaseContext.Companies.Add(company);
        mockDatabaseContext.Jobs.Add(job);
        mockDatabaseContext.Applications.Add(application);
        mockDatabaseContext.SaveChanges(); // Save changes to the in-memory database
    }
}