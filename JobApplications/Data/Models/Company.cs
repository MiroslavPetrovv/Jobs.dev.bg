using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplications.Data.Models
{
    using static JobApplications.Data.DataValidation.CompanyConstants;
    public class Company 
    {
                // ADD ATRIBUTES FOR STRING LENGTH 

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(
            CompanyNameMaxLength,
            MinimumLength = CompanyNameMinLength,
            ErrorMessage = "The field Brand must be minumum {0} length and maximum {1} length! ")]
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
