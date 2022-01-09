using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;
        private readonly INotyfService _notyfService;
        public DeleteModel(sem1.Data.ApplicationDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Item
                .Include(i => i.Product)
                .Include(i => i.Warehouse).FirstOrDefaultAsync(m => m.Id == id);

            if (Item == null)
            {
                return NotFound();
            }
            Warehouse = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == Item.WarehouseId);
            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == Item.ProductId);
            return Page();
        }
        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Item.FindAsync(id);

            if (Item != null)
            {
                Warehouse = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == Item.WarehouseId);
                Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == Item.ProductId);
                Warehouse.Volume = Warehouse.Volume + (Product.Length * Product.Width * Product.Height);
                _context.Attach(Warehouse).State = EntityState.Modified;
                _context.Item.Remove(Item);
                await _context.SaveChangesAsync();
                _notyfService.Success("Item Deleted Successfully", 5);
            }

            return RedirectToPage("./Index");
        }
    }
}
