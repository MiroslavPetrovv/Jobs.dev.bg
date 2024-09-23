using JobApplications.Data;
using JobApplications.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace JobApplications.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext data;

        public JobController(ApplicationDbContext context)
        {
            data = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Job Get()
        {
            return this.data.Jobs.FirstOrDefault();
        }

        public IActionResult GetAll() => View(this.data.Jobs.ToList());

        [HttpGet]
        public IActionResult Add()
        {
            Job job = new Job();
            return View(job);
        }

        [HttpPost]
        public IActionResult Add(Job job)
        {
            if (String.IsNullOrEmpty(job.Title))
            {
                
            }
            if (String.IsNullOrEmpty(job.Salary.ToString()))
            {

            }
            if (String.IsNullOrEmpty(job.Company))
            {

            }
            data.Add(job);
            data.SaveChanges();
            return View();
        }

        public IActionResult Delete(int id)
        {
            Job jobToDelete = data.Jobs.FirstOrDefault(x => x.Id == id);
            data.Jobs.Remove(jobToDelete);
            data.SaveChanges();
            return View(this.data.Jobs.ToList());
        }
        //[HttpGet]
        //public IActionResult Edit(Job job)
        //{
        //    Job jobToUpdate = data.Jobs.FirstOrDefault(x => x.Id == job.Id);


        //    return View(job);
        //}
        //[HttpPost]
        //public IActionResult Edit(Job job)
        //{
        //    if (String.IsNullOrEmpty(job.Title))
        //    {

        //    }
        //    if (String.IsNullOrEmpty(job.Salary.ToString()))
        //    {

        //    }
        //    if (String.IsNullOrEmpty(job.Company))
        //    {

        //    }

        //    data.SaveChanges();
        //    return View();
        //}
    }
}
