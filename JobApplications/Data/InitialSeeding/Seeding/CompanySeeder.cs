using JobApplications.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class CompanySeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            string userId = data.Users.First(x => x.Email == "Company@abv.bg").Id;


            var company1 = AddCompanyInDb(data, 11, "HogwartzAcademy", 5, "12-08-2023", 1, userId);
            

            data.Database.OpenConnection();
            try
            {
                data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON");
                data.SaveChanges();
                data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories OFF");
            }
            finally
            {
                data.Database.CloseConnection();
            }
        }
        private Company AddCompanyInDb(ApplicationDbContext data, int id, string companyName
            , int numberOfEmployees, string DateOfCreation, int industryId, string IdentityUserId)
        {
            Company company = new Company()
            {
                Id = id,
                IdentityUserId = IdentityUserId,
                IndustryId = industryId,
                CompanyName = companyName,
                DateOfCreation = DateOfCreation,
                NumbersOfEmployes = numberOfEmployees,
                PostedJobs = new List<Job>(),
                
                
            };

            data.Companies.Add(company);
            return company;
        }
    }
}
