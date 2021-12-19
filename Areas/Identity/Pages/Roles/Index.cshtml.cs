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
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        public RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IList<IdentityRole> roles { get; set; }

        //public async Task OnGetAsync()
        //{
        //    //Product = await _context.Product.ToListAsync();
        //    roles = _roleManager.Roles.ToList < IdentityRole > ();
        //}
        public void OnGet()
        {
            roles = _roleManager.Roles.ToList<IdentityRole>();
        }
    }
}
