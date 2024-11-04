using JobApplications.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobApplications.DTOs
{
    public class JobFormDto
    {
        public string Title { get; set; } = null!;

        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        public string Description { get; set; } = null!;
    }
}
