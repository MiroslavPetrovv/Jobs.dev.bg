using JobApplications.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class UsersSeeder :ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var emails = new List<string>() { "firstUser@abv.bg", "Company@abv.bg","Admin@abv.bg" };

            if (!data.Users.Any(u => u.Email == emails.First() && u.Email == emails.Last()))
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                foreach (var email in emails)
                {
                    string password;
                    if (email == "firstUser@abv.bg")
                    {
                        password = "Qw12234@";
                    }
                    else if(email == "Company@abv.bg")
                    {
                        password = "Wq12345@";
                    }
                    else
                    {
                        password = "Eq12345@";
                    }

                    Task.
                        Run(async () =>
                        {
                            var user = new IdentityUser()
                            { Email = email, UserName = email };
                            await userManager.CreateAsync(user, password);
                        })
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }
    }
}
