using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Items
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
        ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
        ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }

        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            DateTime a = DateTime.Now;
            Item.CreatedDate = DateTime.Now;
            //if(Item.CreatedDate>=Item.ExpiryDate)
            //{
            //    _notyfService.Error("Expiry Date must be less than than Current Date",5);
            //}
            //else
            {
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
                    _notyfService.Success("Item Added Successfully", 5);
                    return RedirectToPage("./Index");
                }
                _notyfService.Error("Item Volume is greater than Available Volume", 5);
            }


            return RedirectToPage("./Index");
        }
    }
}
