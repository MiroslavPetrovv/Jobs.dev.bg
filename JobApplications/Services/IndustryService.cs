using AutoMapper;
using JobApplications.Data;
using JobApplications.Data.Models;
using JobApplications.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JobApplications.Services
{
    public class IndustryService : IIndustrieService
    {
        private readonly ApplicationDbContext dbContext; 
        

        public IndustryService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public List<Industry> GetAll()
        {
            return dbContext.Industries.ToList();
            
        }

        public async Task<List<Industry>> GetAllAsync()
        {
            List<Industry> industries =await dbContext.Industries.ToListAsync();
            return industries;
        }
    }
}
