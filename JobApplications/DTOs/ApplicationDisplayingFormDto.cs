namespace JobApplications.DTOs
{
    public class ApplicationDisplayingFormDto
    {

        public int Id { get; set; }
        public int JobId { get; set; }

        public string? JobTitle { get; set; } 

        public string IdentityUserId { get; set; }

        public string? UserName { get; set; } 

        public DateTime ApplyedDate { get; set; }

        public int StatusId { get; set; }

        public string? StatusName { get; set; } 

        public string? CvFileDownloadUrl => $"/Company/Application/DownloadCv/{Id}";
    }
}

