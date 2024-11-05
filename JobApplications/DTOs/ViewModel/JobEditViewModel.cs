namespace JobApplications.DTOs.ViewModel
{
    public class JobEditViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Title { get; set; } = null!;

        public decimal Salary { get; set; }

        public int CompanyId { get; set; }

        public bool IsAvaliable { get; set; }

        public int WorkingHours { get; set; }
    }
}
