namespace JobApplications.DTOs
{
    public class JobFilterParams
    {
        public string? Title { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public int? MinWorkingHours { get; set; }
        public int? MaxWorkingHours { get; set; }
        public bool? IsAvailable { get; set; } = true;
    
        public DateTime? PostedAfter { get; set; }
    }
}
