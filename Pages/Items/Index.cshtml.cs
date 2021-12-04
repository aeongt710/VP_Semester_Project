using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public IndexModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public int SerachProductName { get; set; }
        [BindProperty]
        public IList<Item> Item { get;set; }

        public async Task OnGetAsync()
        {
            Item = await _context.Item
                .Include(i => i.Product)
                .Include(i => i.Warehouse).ToListAsync();
        }
        //public async Task OnGetAsync(string ProductName)
        //{
        //    Item = await _context.Item
        //        .Include(i => i.Product)
        //        .Include(i => i.Warehouse).ToListAsync();
            
        //}

        //public async Task<IActionResult> OnPostAsync(string ProductName)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }


        //    return RedirectToAction("", new
        //    {
        //        ID = ProductName
        //    });

        //}
    }
}
