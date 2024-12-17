using Microsoft.AspNetCore.Identity;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class AdminSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var admin = data.Users.FirstOrDefault(e => e.Email == "Admin@abv.bg");
            if (admin !=null)
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                Task
                    .Run(async () =>
                    {
                        
                        await userManager.AddToRoleAsync(admin, "Admin");
                    })
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
