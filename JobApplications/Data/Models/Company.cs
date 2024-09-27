using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobApplications.Data.Models
{
    public class Company 
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;

        public string Industry { get; set; } = null!;

        public int NumbersOfEmployes { get; set; }

        public string Description { get; set; } = null!;

        public string DateOfCreation { get; set; } = null!;



        public List<Job> PostedJobs { get; set; } = new List<Job>();
    }
}
