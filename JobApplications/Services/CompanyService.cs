using AutoMapper;
using JobApplications.Data;
using JobApplications.DTOs;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using JobApplications.Data.Models;
using System.Globalization;
namespace JobApplications.Services
{
    using static JobApplications.Data.DataValidation.CompanyConstants;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection.Metadata.Ecma335;

    public class CompanyService : ICompanyService
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CompanyService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task AddAsync(CompanyFormDTO companyDto)
        {


            //finish the dataConstraits for company
            if (IsValidCompany(companyDto, out string validateError))
            {
                throw new ArgumentException(validateError);
            }

            var company = mapper.Map<Company>(companyDto);
            if (company == null)
            {
                throw new InvalidCastException("Invalid Copmany");
            }


            await this.dbContext.AddAsync(company);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {


            // Validate company for deletion
            var (isValid, validationError) = await IsValidCompanyForDeletion(id);
            if (!isValid)
            {
                throw new ArgumentException(validationError);
            }


            var company = await this.dbContext.Companies.FindAsync(id);
            if (company != null)
            {
                dbContext.Companies.Remove(company);
                await dbContext.SaveChangesAsync();
            }



        }

        public async Task EditAsync(CompanyFormDTO companyDto)
        {
            if (!IsValidCompany(companyDto, out string validationError))
            {
                throw new ArgumentException(validationError);
            }


            var company = await this.dbContext.Companies
                                              .FindAsync(companyDto.Id);
            if (company == null)
            {
                throw new ArgumentException("Company not found.");
            }


            company.CompanyName = companyDto.CompanyName;
            company.DateOfCreation = companyDto.DateOfCreation;
            company.IndustryId = companyDto.IndustryId;
            company.NumbersOfEmployes = companyDto.NumbersOfEmployes;
            company.IdentityUserId = companyDto.IdentityUserId;

            this.dbContext.Update(company);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<JobFormDto>> GetAllJobsAsync(int companyId)
        {
            if (companyId <= 0) throw new ArgumentException("Invalid company id");

            var jobs = await this.dbContext.Jobs
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .Include(x => x.Company)
                .ToListAsync();

            return mapper.Map<List<JobFormDto>>(jobs);


        }

        public async Task<int> GetByUserID(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentException("Invalid user id");
                }
                var companyId = await this.dbContext.Companies
                    .Where(c => c.IdentityUserId == userId)
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();

                return companyId == 0 ? 0 : companyId;
            }
            catch (ArgumentException)
            {

                return 0;
            }
            
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid Job Id");
            }
            var company = await dbContext.Companies.FindAsync(id);
            if (company == null)
            {
                throw new ArgumentException("No existing Job");
            }
            return company;
        }

        private bool IsValidCompany(CompanyFormDTO companyDto, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate CompanyName
            if (string.IsNullOrEmpty(companyDto.CompanyName) ||
                companyDto.CompanyName.Length < CompanyNameMinLength ||
                companyDto.CompanyName.Length > CompanyNameMaxLength)
            {
                errorMessage = $"CompanyName must be between {CompanyNameMinLength} and {CompanyNameMaxLength} characters.";
                return false;
            }

            // Validate DateOfCreation     
            if (string.IsNullOrWhiteSpace(companyDto.DateOfCreation) ||
                !DateTime.TryParseExact(companyDto.DateOfCreation, new[] { "dd-MM-yyyy", "dd.MM.yyyy" }, null, DateTimeStyles.None, out _))
            {
                errorMessage = "Invalid DateOfCreation. Please use the format 'dd-MM-yyyy'.";
                return false;
            }

            // Validate NumbersOfEmployes
            if (companyDto.NumbersOfEmployes < 0)
            {
                errorMessage = "NumbersOfEmployes must be zero or a positive number.";
                return false;
            }

            // Validate Industry
            if (companyDto.IndustryId <= 0) // Assuming 0 or negative values represent an invalid industry.
            {
                errorMessage = "Invalid Industry. IndustryId must be a positive number.";
                return false;
            }

            return true;
        }
        private async Task<(bool, string)> IsValidCompanyForDeletion(int id)
        {
            string errorMessage = string.Empty;

            // Validate id
            if (id <= 0)
            {
                errorMessage = "Invalid company ID. It must be greater than zero.";
                return (false, errorMessage);
            }

            // Validate company existence
            var company = await this.dbContext.Companies.FindAsync(id);
            if (company == null)
            {
                errorMessage = "Company not found. Cannot perform deletion.";
                return (false, errorMessage);
            }

            // Additional conditions can be added here if necessary, e.g., preventing deletion if there are active jobs, etc.

            return (true, errorMessage);
        }

    }
}
