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

namespace sem1.Pages.Warehouses
{
    public class DetailsModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public DetailsModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public IList<Product> AllProducts { get; set; }
        public Warehouse Warehouse { get; set; }
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
            
            if(ItemsInWarehouse != null)
            {
                foreach (Item item in ItemsInWarehouse)
                {
                    Product product = _context.Product.FirstOrDefault(m => m.Id == item.ProductId);
                    CalVolume = CalVolume + (product.Length * product.Width * product.Height);
                }
            }
        }
    }
}
