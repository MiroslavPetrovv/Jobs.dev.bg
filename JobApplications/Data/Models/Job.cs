using System.ComponentModel.DataAnnotations;

namespace JobApplications.Data.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Company { get; set; } = null!;
    }
}
