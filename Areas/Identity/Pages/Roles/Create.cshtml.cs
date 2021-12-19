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
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {

        public RoleManager<IdentityRole> _roleManager;
        [BindProperty] public string RoleName { get; set; }
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_roleManager.RoleExistsAsync(RoleName).Result)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = RoleName });
            }


            return RedirectToPage("./Index");
        }

    }
}
