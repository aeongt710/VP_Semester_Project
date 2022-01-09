using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Products
{
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
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();
            _notyfService.Success("Product Added Successfully", 5);
            return RedirectToPage("./Index");
        }
    }
}
