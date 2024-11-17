using JobApplications.Data.Models;

namespace JobApplications.DTOs
{
    public class CompanyFormDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } 

        public int IndustryId { get; set; }

        public List<Industry> Industries { get; set; } = new List<Industry>();

        public int NumbersOfEmployes { get; set; }

        public string DateOfCreation { get; set; } 

        public string IdentityUserId { get; set; }
    }
}
