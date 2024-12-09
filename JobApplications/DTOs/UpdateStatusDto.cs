namespace JobApplications.DTOs
{
    public class UpdateStatusDto
    {
        public int ApplicationId { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
