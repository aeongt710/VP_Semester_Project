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
    public class DeleteModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public DeleteModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        //public IList<Warehouse> Warehouses { get; set; }

        public IList<Item> Items { get; set; }

        public Dictionary<int,Warehouse> Warehouses { get; set; }

        public IList<Warehouse> Ware { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            Items = await _context.Item.Include(a=>a.Warehouse).Where(m => m.ProductId == id).ToListAsync();
            
            foreach(var item in Items)
            {
                Ware.Add(item.Warehouse);
            }

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FindAsync(id);

            if (Product != null)
            {
                _context.Product.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
