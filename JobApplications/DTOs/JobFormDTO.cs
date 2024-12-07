using JobApplications.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobApplications.DTOs
{
    public class JobFormDto 
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        public Company? Company { get; set; }

        public string Description { get; set; } = null!;

        public string JobTitleDescription { get; set; } = null!;

        public int WorkingHours { get; set; }

        public bool IsAvaliable { get; set; }
        
        public string? Banner { get; set; }

    }
}
