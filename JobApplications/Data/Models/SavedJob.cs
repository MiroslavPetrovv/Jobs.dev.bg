using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplications.Data.Models
{
    public class SavedJob
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentityUserId { get; set; }

        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser IdentityUser { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }

        public DateTime SavedDate { get; set; } 
    }
}
