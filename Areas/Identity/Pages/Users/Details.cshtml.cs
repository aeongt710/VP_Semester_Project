using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Areas.Identity.Pages.Users
{
    public class DetailsModel : PageModel
    {
        public RoleManager<IdentityRole> roleManager;
        public UserManager<IdentityUser> userManager;
        public IdentityRole Role { get; set; }
        public IList<IdentityUser> AllRoleUsers;

        public DetailsModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Role = await roleManager.FindByIdAsync(id);

            //Student = await _context.Student.FirstOrDefaultAsync(m => m.StudentID == id);

            if (Role == null)
            {
                return NotFound();
            }
            // AllUsers= userManager.Users.ToList();
            AllRoleUsers = await userManager.GetUsersInRoleAsync(Role.Name);
            return Page();
        }
    }
}
