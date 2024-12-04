﻿namespace JobApplications.Areas.HR.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    [Area("Company")]
    //[Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Company")]
    
    public class BaseController : Controller
    {
    }
}