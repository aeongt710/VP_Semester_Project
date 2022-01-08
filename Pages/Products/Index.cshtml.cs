using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public IndexModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        [BindProperty]
        [Display(Name = "Enter Product Name")]
        public String SearchName { get; set; }
        public async Task OnGetAsync(string taskof,string SearchName)
        {
            if(taskof== "SearchProductName")
            {
                Product = await _context.Product.Where(m=>m.Name.ToLower()==SearchName.ToLower()).ToListAsync();
            }
            else
            {
                Product = await _context.Product.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync(string SearchName, string taskof)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToAction("", new { SearchName = SearchName, TaskOf = taskof });

        }
    }
}
