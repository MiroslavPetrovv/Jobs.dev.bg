using JobApplications.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JobApplications.DTOs.ViewModel.JobViewModels;

namespace JobApplications.DTOs
{
    public class JobFormDto :  JobEditViewModel
    {
        public string Title { get; set; } = null!;

        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        public string Description { get; set; } = null!;

        public int WorkingHours { get; set; }

        public int Id { get; set; }

        public bool IsAvaliable { get; set; }
    }
}
