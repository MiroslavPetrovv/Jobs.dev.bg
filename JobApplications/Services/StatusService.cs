using AutoMapper;
using JobApplications.Data;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobApplications.Services
{
    public class StatusService : IStatusService
    {
        private readonly ApplicationDbContext dbContext;


        public StatusService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;


        }

        public async Task<bool> UpdateApplicationStatusAsync(int applicationId, string statusName,int statusId)
        {
            var application = await dbContext.Applications.Include(a=>a.Status).FirstAsync(a=>a.Id==applicationId);
            if (application == null)
            {
                throw new ArgumentException("Application not found.");
            }

            application.Status.ApplicationStatus = statusName;
            application.StatusId = statusId; 
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
