using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sem1.Data;
using sem1.Models;


namespace sem1.Pages.Warehouses
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;
        private readonly INotyfService _notyfService;

        public CreateModel(sem1.Data.ApplicationDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Warehouse Warehouse { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Warehouse.Add(Warehouse);
            await _context.SaveChangesAsync();
            _notyfService.Success("Warehouse Creadted Sucessfully!",5);

            return RedirectToPage("./Index");
        }
    }
}
