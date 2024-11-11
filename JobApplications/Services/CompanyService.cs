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

        public async Task Add(CompanyFormDTO companyDto)
        {

            var company =mapper.Map<Company>(companyDto);
            if(company == null)
            {
                throw new InvalidCastException("Invalid Copmany");
            }
            //finish the dataConstraits for company
            if (string.IsNullOrEmpty(companyDto.CompanyName)) //companyDto.CompanyName<=)
            {
                throw new ArgumentException("Invalid companyName");
            }
            
            
            await this.dbContext.AddAsync(company);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id ==0)
            {
                throw new ArgumentException("Invalid user id");
            }

            Company company = await dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (company == null)
            {
                throw new InvalidOperationException("Company Not Found");
            }
            dbContext.Companies.Remove(company);
            dbContext.SaveChangesAsync();
            
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
