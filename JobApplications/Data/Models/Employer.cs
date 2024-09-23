namespace JobApplications.Data.Models
{
    public class Employer : User
    {
        public string CompanyName { get; set; } = null!;

        public string Industry { get; set; } = null!;

        //public List<JobPosting> PostedJobs { get; set; }
    }
}
