using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Warehouses
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
        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
        public IList<Product> AllProducts { get; set; }
        public IList<Item> ItemsInWarehouse { get; set; }

        [Display(Name = "Occupied Volume")]
        public int CalVolume { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == id);
            AllProducts = await _context.Product.ToListAsync();
            ItemsInWarehouseFunction();
            if (Warehouse == null)
            {
                return NotFound();
            }
            return Page();
        }
        public void ItemsInWarehouseFunction()
        {
            CalVolume = 0;
            ItemsInWarehouse = _context.Item.Where(m => m.WarehouseId == Warehouse.Id).ToList();

            if (ItemsInWarehouse != null)
            {
                foreach (Item item in ItemsInWarehouse)
                {
                    Product product = _context.Product.FirstOrDefault(m => m.Id == item.ProductId);
                    CalVolume = CalVolume + (product.Length * product.Width * product.Height);
                }
            }
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse = await _context.Warehouse.FindAsync(id);

            if (Warehouse != null)
            {
                _context.Warehouse.Remove(Warehouse);
                await _context.SaveChangesAsync();
                _notyfService.Success("Warehouse Deleted Successfully!", 5);
            }

            return RedirectToPage("./Index");
        }
    }
}
