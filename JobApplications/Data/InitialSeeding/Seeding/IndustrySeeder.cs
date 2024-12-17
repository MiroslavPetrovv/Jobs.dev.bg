using JobApplications.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class IndustrySeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Industries.Any(d => d.Id == 1))
            {
                var category1 = AddIndustryInDb(data, 1, "Technology", "Industry focused on technology and innovation");
                var category2 = AddIndustryInDb(data, 2, "Healthcare", "Industry focused on health services and products");
                var category3 = AddIndustryInDb(data, 3, "Manufacturing", "Industry involved in production and manufacturing");
                var category4 = AddIndustryInDb(data, 4, "Finance", "Industry dealing with finance and investment");
                //var category5 = AddIndustryInDb(data, 5, "SUV");
                //var category6 = AddIndustryInDb(data, 6, "Vans");
                //var category7 = AddIndustryInDb(data, 7, "Luxury");

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
        }

        private Industry AddIndustryInDb
            (ApplicationDbContext data, int id, string name,string description)
        {
            Industry category = new Industry()
            {
                Id = id,
                Name = name,
                Description = description
            };

            data.Industries.Add(category);

            return category;
        }
    }
}
