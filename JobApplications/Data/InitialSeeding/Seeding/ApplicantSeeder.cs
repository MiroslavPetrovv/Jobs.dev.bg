using Microsoft.AspNetCore.Identity;

namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class ApplicantSeeder: ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var applicant = data.Users.FirstOrDefault(e => e.Email == "firstUser@abv.bg");
            if (applicant != null)
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                Task
                    .Run(async () =>
                    {

                        await userManager.AddToRoleAsync(applicant, "Applicant");
                    })
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
