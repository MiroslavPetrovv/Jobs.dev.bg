using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplications.Data.Models
{
    public class Application 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobId { get; set; }

        
        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }

        [Required]
        public string IdentityUserId { get; set; } = null!;

        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser IdentityUser { get; set; }

        [Required]
        public DateTime ApplyedDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }

        public string CvFilePath { get; set; } = null!;

    }
}
