using AutoMapper;
using JobApplications.Data.Models;
using JobApplications.DTOs;
using System.Runtime.CompilerServices;

namespace JobApplications.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CompanyFormDTO, Company>();
            this.CreateMap<Company, CompanyFormDTO>();
            this.CreateMap<Job, JobFormDto>();
            this.CreateMap<JobFormDto, Job>();
            this.CreateMap<Application, ApplicationFormDto>();
            this.CreateMap<ApplicationFormDto, Application>();

        }
    }
}
