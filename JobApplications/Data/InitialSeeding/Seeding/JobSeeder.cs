using JobApplications.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class JobSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Industries.Any(d => d.Id == 1))
            {
                var job1 = AddJobInDb(data, 1, "Software Developer", "Develop software applications", "Responsible for developing and maintaining software.", DateTime.Now, 40, 60000, 11, "https://storage.googleapis.com/a1aa/image/cfe4ea6d-d220-48fe-b42b-793d045a476f.jpeg");
                var job2 = AddJobInDb(data, 2, "Project Manager", "Manage projects", "Oversee project development and ensure timely delivery.", DateTime.Now, 40, 80000, 11, "https://storage.googleapis.com/a1aa/image/cfe4ea6d-d220-48fe-b42b-793d045a476f.jpeg");
                var job3 = AddJobInDb(data, 3, "Data Analyst", "Analyze data", "Collect and analyze data to help make business decisions.", DateTime.Now, 40, 55000, 11, "https://storage.googleapis.com/a1aa/image/cfe4ea6d-d220-48fe-b42b-793d045a476f.jpeg");
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

        private Job AddJobInDb
            (ApplicationDbContext data, int id, string title, string jobTitleDescription,string description,DateTime PostedDate,int workingHours,int salary,int companyId,string banner)
        {
            Job job = new Job()
            {
                Id = id,
                JobTitleDescription =jobTitleDescription,
                Description = description,
                Title = title,
                IsAvaliable = true,
                Banner = banner ,
                CompanyId = companyId,
                PostedDate = PostedDate,
                WorkingHours = workingHours,
                Salary = salary,
                Applications = new List<Application>()
            };

            data.Jobs.Add(job);

            return job;
        }
    }
}

