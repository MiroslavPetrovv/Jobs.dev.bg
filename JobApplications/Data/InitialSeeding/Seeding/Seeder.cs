namespace JobApplications.Data.InitialSeeding.Seeding
{
    public class Seeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var seeders = new List<ISeeder>()
            {
                new RoleSeeder(),
                new UsersSeeder(),
                new AdminSeeder(),
                new CompanyOwnerSeeder(),
                new CompanySeeder(),
                new IndustrySeeder(),
                new ApplicantSeeder(),
            };

            foreach (var seeder in seeders)
            {
                seeder.Seed(data, serviceProvider);
            }
        }
    }
}
