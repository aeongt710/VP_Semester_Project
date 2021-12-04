using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Areas.Identity.Pages.Roles
{
    public class CreateModel : PageModel
    {

        public CreateModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if(!_roleManager.RoleExistsAsync(roleName: RoleName))
            await _roleManager.CreateAsync(new IdentityRole() { Name = RoleName.ToString() });
            return Page();
        }
        public RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public string RoleName { get; set; }

    }
}
