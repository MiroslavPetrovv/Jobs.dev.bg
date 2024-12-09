using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.Extensions;
using JobApplications.Services;
using JobApplications.Services.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplication.Tests.Services
{
    internal class IndustryServiceTests
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

        [TearDown]
        public void Teardown()
        {
            dbContext.Dispose(); // Dispose of the in-memory database after each test
        }

        [Test]
        public async Task GetAllShouldReturnAllIndustries()
        {
            // Arrange
            var expectedIndustryCount = 9;
            var service = new IndustryService(mockDatabaseContext); // Use the mock database context

            // Act
            var allIndustries = await service.GetAllAsync();

            // Assert
            Assert.That(allIndustries.Count, Is.EqualTo(expectedIndustryCount));
        }

        private void SeedDatabase()
        {
            // Add seed data to the in-memory database
            var industries = new List<Industry>
            {
                new Industry { Id = 51, Name = "Technology", Description = "Industry focused on technology and innovation" },
                new Industry { Id = 52, Name = "Healthcare", Description = "Industry focused on health services and products" },
                new Industry { Id = 53, Name = "Manufacturing", Description = "Industry involved in production and manufacturing" },
                new Industry { Id = 54, Name = "Finance", Description = "Industry dealing with finance and investment" },
                new Industry { Id = 55, Name = "Education", Description = "Industry related to education and training" },
                new Industry { Id = 56, Name = "Retail", Description = "Industry focused on selling goods and services" },
                new Industry { Id = 57, Name = "Construction", Description = "Industry related to building and construction" },
                new Industry { Id = 58, Name = "Transportation", Description = "Industry involving the movement of goods and people" },
                new Industry { Id = 59, Name = "Hospitality", Description = "Industry related to services in hotels, restaurants, and tourism" }
            };

            mockDatabaseContext.Industries.AddRange(industries);
            mockDatabaseContext.SaveChanges();
        }
    }
}
