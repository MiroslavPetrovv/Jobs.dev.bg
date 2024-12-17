namespace JobApplications.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Area("Admin")]
    //[Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Admin")]
    public class BaseAdminController : Controller
    {
        protected bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }

        protected string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
