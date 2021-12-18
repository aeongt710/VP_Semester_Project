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
    public class EditModel : PageModel
    {

        private readonly UserManager<IdentityUser> userManager;

        public EditModel( UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public IdentityUser user { get; set; }

        public async Task<IActionResult> OnGetAsync(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Page();
        }
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            ////await userManager.UpdateAsync(user);
            //await userManager.UpdateAsync(user);

            ////return RedirectToAction("", new
            ////{
            ////    ID = ID
            ////});
            //return Page();




            if (!ModelState.IsValid)
            {
                return Page();
            }

            await userManager.UpdateAsync(user);


            return Page();
        }
    }
}
