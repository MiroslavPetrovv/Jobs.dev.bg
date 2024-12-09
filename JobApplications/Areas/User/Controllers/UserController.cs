namespace JobApplications.Areas.User.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    [Area("User")]
    //[Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Applicant")]

    public class UserController : Controller
    {
    }
}