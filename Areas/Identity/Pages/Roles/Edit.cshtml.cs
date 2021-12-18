using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Areas.Identity.Pages.Roles
{
    public class EditModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        public EditModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;


        }

        [BindProperty]
        public IdentityRole Role { get; set; }
        public IList<IdentityUser> identityUsers;
        public IList<IdentityUser> identityRoleUsers;
        public async Task<IActionResult> OnGetAsync(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await roleManager.FindByIdAsync(id);

            if (Role == null)
            {
                return NotFound();
            }
            identityUsers = userManager.Users.ToList();
            identityRoleUsers = await userManager.GetUsersInRoleAsync(Role.Name);

            return Page();
        }
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string User, string RoleID, string role, string taskof)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (taskof == "AddUser")
            {
                await userManager.AddToRoleAsync(await userManager.FindByIdAsync(User), role);
            }
            if (taskof == "RemoveUser")
            {

                await userManager.RemoveFromRoleAsync(await userManager.FindByIdAsync(User), role);
            }
            return RedirectToAction("", new
            {
                ID = RoleID
            });

        }

    }
}
