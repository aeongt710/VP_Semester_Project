using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public DetailsModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Item> Items { get; set; }

        public HashSet<Warehouse> Ware;
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            Items = _context.Item.Include(a => a.Warehouse)
                .Where(m => m.ProductId == id)
                .ToListAsync().Result;
            Ware = new HashSet<Warehouse>();

            foreach (var item in Items)
            {
                Ware.Add(item.Warehouse);
            }
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
