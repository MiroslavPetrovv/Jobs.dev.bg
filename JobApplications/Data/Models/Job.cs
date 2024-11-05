using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplications.Data.Models
{
    using static JobApplications.Data.DataValidation.Job;
    public class Job
    {
        // ADD ATRIBUTES FOR STRING LENGTH 

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(
            JobTitleMaxLength,
            MinimumLength = JobTitleMinLength,
            ErrorMessage = "The field Brand must be minumum {0} length and maximum {1} length! ")]
        public string Title { get; set; } = null!;

        [Required]
        //[Range(MinSalaryFor8HourShift,)]
        [Display(Name ="Montly Salary")]
        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;

        [Required]
        [StringLength(
            JobDescriptionMaxLength,
            MinimumLength =JobDescriptionMinLegnth,
            ErrorMessage = "The field Brand must be minumum {0} length and maximum {1} length! ")]
        public string Description { get; set; } = null!;

        public IList<Application> Applications { get; set; } = new List<Application>();

        [Required]
        public bool IsAvaliable { get; set; }

        [Required]
        public int WorkingHours { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }



    }
}
