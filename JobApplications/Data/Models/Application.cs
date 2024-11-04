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

        
        public string IdentityUserId { get; set; }

        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser IdentityUser { get; set; }

        public DateTime ApplyedDate { get; set; }

        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }

        //To Do SV

    }
}
