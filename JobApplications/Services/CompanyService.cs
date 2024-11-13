using AutoMapper;
using JobApplications.Data;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JobApplications.Data.Models;
namespace JobApplications.Services
{
    using static JobApplications.Data.DataValidation.CompanyConstants;
    using JobApplications.Data.DataValidation;
    using System.Collections.Generic;

    public class CompanyService : ICompanyService
    {
        
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CompanyService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Add(CompanyFormDTO companyDto)
        {

            var company = mapper.Map<Company>(companyDto);
            if (company == null)
            {
                throw new InvalidCastException("Invalid Copmany");
            }
            //finish the dataConstraits for company
            if (string.IsNullOrEmpty(companyDto.CompanyName) || companyDto.CompanyName.Count()<CompanyNameMinLength
                || companyDto.CompanyName.Count()>CompanyNameMaxLength)
            {
                throw new ArgumentException("Invalid companyName");
            }


            await this.dbContext.AddAsync(company);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid user id");
            }

            Company company = await this.dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (company == null)
            {
                throw new InvalidOperationException("Company Not Found");
            }
            dbContext.Companies.Remove(company);
            await dbContext.SaveChangesAsync();

        }

        public async Task Edit(CompanyFormDTO companyDto)
        {
            if (string.IsNullOrEmpty(companyDto.CompanyName) || companyDto.CompanyName.Count() < CompanyNameMinLength
                || companyDto.CompanyName.Count() > CompanyNameMaxLength)
            {
                throw new ArgumentException("Invalid companyName");
            }

            var company = mapper.Map<Company>(companyDto);
            if (company == null)
            {
                throw new InvalidCastException("Invalid Copmany");
            }
            this.dbContext.Update(company);
            await this.dbContext.SaveChangesAsync();
  
        }

        public async Task<List<JobFormDto>> GetAllJobs(int companyId)
        {
            var jobs = await this.dbContext.Jobs.Where(x => x.CompanyId == companyId).Include(x=> x.Company).ToListAsync();
            var jobsDto = mapper.Map<List<JobFormDto>>(jobs);

            return jobsDto;
        }

        public async Task<int> GetByUserID(string userId)
        {
            var company = await this.dbContext.Companies.FirstOrDefaultAsync(c => c.IdentityUserId == userId);

            return company == null ? 0 : company.Id;
        }
    }
}
