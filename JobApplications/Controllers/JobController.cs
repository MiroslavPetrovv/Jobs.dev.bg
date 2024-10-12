using JobApplications.Data;
using JobApplications.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace JobApplications.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext data;

        public JobController(ApplicationDbContext context)
        {
            data = context;
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
            
            if (String.IsNullOrEmpty(job.Title) || String.IsNullOrEmpty(job.Salary.ToString()))
            {
                
            }
            if (job.Title.Length<=5 || job.Salary.ToString().Length <=2)
            {

            }
            //make a drop down menu for the company
            data.Add(job);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }

        public IActionResult Delete(int id)
        {
            Job jobToDelete = data.Jobs.FirstOrDefault(x => x.Id == id);
            data.Jobs.Remove(jobToDelete);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Job jobToUpdate = data.Jobs.FirstOrDefault(x => x.Id == id);


            return View(jobToUpdate);
        }
        [HttpPost]
        public IActionResult Edit(Job job)
        {
            if (String.IsNullOrEmpty(job.Title))
            {

            }
            if (String.IsNullOrEmpty(job.Salary.ToString()))
            {

            }
            
            data.Jobs.Update(job);
            data.SaveChanges();
            return RedirectToAction("GetAll");
        }
    }
}
