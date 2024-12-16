namespace JobApplications.Data.InitialSeeding.Seeding
{
    public interface ISeeder
    {
        void Seed(ApplicationDbContext data, IServiceProvider serviceProvider);
    }
}
