using JobApplications.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new Industry { Id = 1, Name = "Technology", Description = "Industry focused on technology and innovation" };
            new Industry { Id = 2, Name = "Healthcare", Description = "Industry focused on health services and products" };
            new Industry { Id = 3, Name = "Manufacturing", Description = "Industry involved in production and manufacturing" };
            new Industry { Id = 4, Name = "Finance", Description = "Industry dealing with finance and investment" };
            new Industry { Id = 5, Name = "Education", Description = "Industry related to education and training" };
            new Industry { Id = 6, Name = "Retail", Description = "Industry focused on selling goods and services" };
            new Industry { Id = 7, Name = "Construction", Description = "Industry related to building and construction" };
            new Industry { Id = 8, Name = "Transportation", Description = "Industry involving the movement of goods and people" };
            new Industry { Id = 9, Name = "Hospitality", Description = "Industry related to services in hotels, restaurants, and tourism" };
            new Industry { Id = 10, Name = "Agriculture", Description = "Industry related to farming and food production" };
            new Industry { Id = 11, Name = "Energy", Description = "Industry focused on energy production and distribution" };
            new Industry { Id = 12, Name = "Telecommunications", Description = "Industry related to communication technologies" };
            new Industry { Id = 13, Name = "Automotive", Description = "Industry focused on manufacturing and selling vehicles" };
            new Industry { Id = 14, Name = "Real Estate", Description = "Industry related to property management and sales" };
            new Industry { Id = 15, Name = "Entertainment", Description = "Industry related to media, movies, and recreational activities" };
            new Industry { Id = 16, Name = "Pharmaceuticals", Description = "Industry focused on drug development and healthcare products" };
            new Industry { Id = 17, Name = "Aerospace", Description = "Industry related to the development of aircraft and spacecraft" };
            new Industry { Id = 18, Name = "Media", Description = "Industry involving news, publishing, and broadcasting" };
            new Industry { Id = 19, Name = "Food and Beverage", Description = "Industry related to the production and distribution of food and drinks" };
            new Industry { Id = 20, Name = "Insurance", Description = "Industry providing risk management and insurance services" };
            new Industry { Id = 21, Name = "Legal", Description = "Industry providing legal services and representation" };
            new Industry { Id = 22, Name = "Tourism", Description = "Industry focused on travel and tourism services" };
            new Industry { Id = 23, Name = "Nonprofit", Description = "Industry focused on charitable organizations and services" };
            new Industry { Id = 24, Name = "Government", Description = "Industry related to governmental services and administration" };
            new Industry { Id = 25, Name = "Mining", Description = "Industry involved in the extraction of minerals and resources" };
            new Industry { Id = 26, Name = "Chemical", Description = "Industry focused on the production of chemicals and related products" };
            new Industry { Id = 27, Name = "Fashion", Description = "Industry related to clothing design and manufacturing" };
            new Industry { Id = 28, Name = "Logistics", Description = "Industry focused on the management of the flow of goods and services" };
            new Industry { Id = 29, Name = "Biotechnology", Description = "Industry focused on biological and life sciences applications" };
            new Industry { Id = 30, Name = "Environmental Services", Description = "Industry focused on sustainability and environmental protection" };
        }
        public DbSet<Job> Jobs { get; set; } = null!;

        public DbSet<Company> Companies { get; set; } = null!;

        public DbSet<Industry> Industries { get; set; }
    }
}
