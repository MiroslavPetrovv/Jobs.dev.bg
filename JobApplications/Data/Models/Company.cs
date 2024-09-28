using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplications.Data.Models
{
    public class Company 
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;

        public int IndustryId { get; set; }

        [Required]
        [ForeignKey(nameof(IndustryId))]
        public Industry Industry { get; set; } = null!;

        public int NumbersOfEmployes { get; set; }

       

        public string DateOfCreation { get; set; } = null!;



        public List<Job> PostedJobs { get; set; } = new List<Job>();
    }
}
