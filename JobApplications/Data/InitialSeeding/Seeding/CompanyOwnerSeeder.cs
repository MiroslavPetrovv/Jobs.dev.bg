using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class CompanyOwnerSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var owner = data.Users.FirstOrDefault(e => e.Email == "Company@abv.bg");
            if (owner != null)
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                Task
                    .Run(async () =>
                    {

                        await userManager.AddToRoleAsync(owner, "Company");
                    })
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
