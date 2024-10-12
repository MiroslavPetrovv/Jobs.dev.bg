using JobApplications.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

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

            builder.Entity<Industry>().HasData(
                new Industry { Id = 1, Name = "Technology", Description = "Industry focused on technology and innovation" },
                new Industry { Id = 2, Name = "Healthcare", Description = "Industry focused on health services and products" },
                new Industry { Id = 3, Name = "Manufacturing", Description = "Industry involved in production and manufacturing" },
                new Industry { Id = 4, Name = "Finance", Description = "Industry dealing with finance and investment" },
                new Industry { Id = 5, Name = "Education", Description = "Industry related to education and training" },
                new Industry { Id = 6, Name = "Retail", Description = "Industry focused on selling goods and services" },
                new Industry { Id = 7, Name = "Construction", Description = "Industry related to building and construction" },
                new Industry { Id = 8, Name = "Transportation", Description = "Industry involving the movement of goods and people" },
                new Industry { Id = 9, Name = "Hospitality", Description = "Industry related to services in hotels, restaurants, and tourism" },
                new Industry { Id = 10, Name = "Agriculture", Description = "Industry related to farming and food production" },
                // Add remaining entries here...
                new Industry { Id = 30, Name = "Environmental Services", Description = "Industry focused on sustainability and environmental protection" }
    );
            base.OnModelCreating(builder);
        }
        public DbSet<Job> Jobs { get; set; } = null!;

        public DbSet<Company> Companies { get; set; } = null!;

        public DbSet<Industry> Industries { get; set; }
    }
}
