using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplications.Data.Models
{
    public class Job
    {
        // ADD ATRIBUTES FOR STRING LENGTH 

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public IList<Application> Applications { get; set; } = new List<Application>();

        public bool IsAvaliable { get; set; }



    }
}
