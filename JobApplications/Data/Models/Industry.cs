using System.ComponentModel.DataAnnotations;

namespace JobApplications.Data.Models
{
    public class Industry
    {
        [Key]
        public int Id { get; set; }   
        public string Name { get; set; } = null!;
    }
}