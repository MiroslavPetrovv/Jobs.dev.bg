
using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;

namespace JobApplications.Data.Models
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;

        [Required]
        public DateTime DateRegistered { get; set; }

        [Required]
        public DateTime LastLogin { get; set; }

        [Required]
        public string Bio { get; set; } = null!;

        [Required]
        public string Resume { get; set; } = null!;

        public List<Job>? Applications { get; set; } 
        public string? PreferredJobCategory { get; set; }
        public string? PreferredLocation { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
    }
}
