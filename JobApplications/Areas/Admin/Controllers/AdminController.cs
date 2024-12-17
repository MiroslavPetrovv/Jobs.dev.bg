namespace JobApplications.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using JobApplications.Controllers;
    using Microsoft.Extensions.Configuration.UserSecrets;
    using JobApplications.Areas.HR.Controllers;

    [Area("Admin")]

    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this._userManager = userManager;
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoleAndUser()
        {
            bool x = await this.roleManager.RoleExistsAsync("Admin");

            if (!x)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                await this.roleManager.CreateAsync(role);


                
                IdentityUser user = this._userManager.Users.FirstOrDefault(e => e.Email == "Admin1@abv.bg");

                //Add default User to Role Admin    
                if (user != null)
                {
                    IdentityResult result1 = await this._userManager.AddToRoleAsync(user, role.Name);
                }
            }
            else
            {
                IdentityUser user = this._userManager.Users.FirstOrDefault(e => e.Email == "Admin1@abv.bg");

                //Add default User to Role Admin    
                if (user != null)
                {
                    IdentityResult result1 = await this._userManager.AddToRoleAsync(user, "Admin");
                }
            }

            
            return RedirectToAction("Home");
        }
    }
}
