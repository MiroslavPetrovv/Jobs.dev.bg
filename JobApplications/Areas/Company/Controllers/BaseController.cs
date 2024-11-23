using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplications.Areas.Company.Controllers
{
    [Area("Admin")]
    //[Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Company")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
