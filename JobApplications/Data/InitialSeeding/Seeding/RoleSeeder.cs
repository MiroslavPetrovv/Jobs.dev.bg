namespace JobApplications.Data.InitialSeeding.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public class RoleSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Admin"))
                        return;

                    var adminRole = new IdentityRole { Name = "Admin" };

                    await roleManager.CreateAsync(adminRole);

                    if (await roleManager.RoleExistsAsync("Company"))
                        return;

                    var companyRole = new IdentityRole { Name = "Company"};

                    if (await roleManager.RoleExistsAsync("Applicant"))
                        return;

                    var userRole = new IdentityRole { Name = "Applicant" };

                    await roleManager.CreateAsync(userRole);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
