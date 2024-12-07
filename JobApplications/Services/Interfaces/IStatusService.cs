namespace JobApplications.Services.Interfaces
{
    public interface IStatusService
    {
        Task<bool> UpdateApplicationStatusAsync(int applicationId, string statusName,int statusId);
    }
}
