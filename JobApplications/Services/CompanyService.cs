using AutoMapper;
using JobApplications.Data;
using JobApplications.DTOs;
using JobApplications.DTOs.ViewModel.CompanyViewModels;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JobApplications.Data.Models;
namespace JobApplications.Services
{
    using JobApplications.Data.DataValidation;
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CompanyService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Add(CompanyFormDTO companyDto,string UserId)
        {

            companyDto.IdentityUserId = UserId;
            if (companyDto.IndustryId == 0)
            {
                throw new ArgumentException("no user with this Id");
            }
            var company =mapper.Map<Company>(companyDto);
            if(company == null)
            {
                throw new InvalidCastException("Invalid Copmany");
            }
            //finish the dataConstraits for company
            if (string.IsNullOrEmpty(companyDto.CompanyName) ) //companyDto.CompanyName<=)
            {
                throw new ArgumentException("Invalid companyName");
            }
            
            
            await this.dbContext.AddAsync(company);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Edit(CompanyEditViewModel CompanyEditViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetByUserID(string userId)
        {
            var company = await this.dbContext.Companies.FirstOrDefaultAsync(c => c.IdentityUserId == userId);
            return company == null ? 0 : company.Id;
        }
    }
}
