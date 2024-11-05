namespace JobApplications.DTOs.ViewModel.CompanyViewModels
{
    public class CompanyEditViewModel
    {
        public  int Id { get; set; }

        public string CompanyName { get; set; } = null!;

        public int IndustryId { get; set; }

        public int NumbersOfEmployes { get; set; }

        public string DateOfCreation { get; set; } = null!;

        public string IdentityUserId { get; set; } = null!;

        
    }
}
