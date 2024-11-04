using System.ComponentModel.DataAnnotations;

namespace JobApplications.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationStatus { get; set; }
    }
}
