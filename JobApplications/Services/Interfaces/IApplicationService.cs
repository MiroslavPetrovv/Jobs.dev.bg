﻿using JobApplications.Data.Models;
using JobApplications.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Services.Interfaces
{
    public interface IApplicationService
    {
        Task ApplyForAJobAsync(ApplicationFormDto applicationDto, string userId,List<IFormFile> CvFile);

        Task<List<ApplicationFormDto>> SeeAllApplications(int jobId);

        Task<ApplicationDownloadDto> GetApplicationByIdForDownloadingCv(int id);

        Task<bool> UpdateApplicationStatusAsync(int applicationId, string statusName,int statusId);

        Task<List<ApplicationDisplayingFormDto>> SeeAllApplicationsForAJobAsync(int jobId);

        Task<List<ApplicationDisplayingFormDto>> SeeAllApplicationByUserId(string userId);
    }
}
