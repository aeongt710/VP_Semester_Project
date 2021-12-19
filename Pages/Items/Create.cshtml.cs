using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Items
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public CreateModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
        ViewData["WarehouseId"] = new SelectList(_context.Set<Warehouse>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Warehouse = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == Item.WarehouseId);
            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == Item.ProductId);
            int wareV = Warehouse.Volume;
            int itemV = Product.Height * Product.Length * Product.Width;
            if (itemV <= wareV)
            {
                Warehouse.Volume = wareV - itemV;
                _context.Attach(Warehouse).State = EntityState.Modified;
                _context.Item.Add(Item);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Create");
        }

    }
}
