using JobApplications.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobApplications.Areas.Company.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string CompanyName { get; set; } = null!;

        [Required]
        public int IndustryId { get; set; }

        [Required]
        [ForeignKey(nameof(IndustryId))]
        public Industry Industry { get; set; } = null!;

        [Required]
        public int NumbersOfEmployes { get; set; }

        [Required]
        public string DateOfCreation { get; set; } = null!;

        // guid
        [Required]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public List<Job> PostedJobs { get; set; } = new List<Job>();
    }
}
