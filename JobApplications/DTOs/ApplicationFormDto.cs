using JobApplications.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace JobApplications.DTOs
{
    public class ApplicationFormDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }

        public Job? Job{ get; set; }

        public string IdentityUserId { get; set; }

        public IdentityUser? IdentityUser{ get; set; }

        public DateTime ApplyedDate { get; set; }

        public int StatusId { get; set; }

        public Status? Status { get; set; }


    }
}
