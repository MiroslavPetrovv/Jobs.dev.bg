using AutoMapper;
using JobApplications.Areas.HR.Controllers;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using JobApplications.Extensions;
using JobApplications.Migrations;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Tests.Controllers
{
    public class CompanyControllerTests
    {
        private InMemoryDbContext dbContext;
        private ApplicationDbContext mockDatabaseContext; // Use this instead of the real database// PISHI KOD MOM4E !!!!!!!!
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
        

        private void SeedDatabase()
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


            mockDatabaseContext.Companies.Add(company);
            mockDatabaseContext.Jobs.Add(job);
            mockDatabaseContext.Applications.Add(application);
        }
    }
}

