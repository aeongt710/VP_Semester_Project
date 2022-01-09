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

namespace sem1.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;
        //public List<Warehouse> Ware { get; set; }
        private readonly INotyfService _notyfService;

        public DeleteModel(sem1.Data.ApplicationDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [BindProperty]
        public Product Product { get; set; }

        //public IList<Warehouse> Warehouses { get; set; }

        [BindProperty]
        public IList<Item> Items { get; set; }

        public HashSet<Warehouse> Ware;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           // Items = new List<Item>();
            //Product = await Task.FromResult(_context.Product.FirstOrDefaultAsync(m => m.Id == id)).Result;
            Product = _context.Product.FirstOrDefaultAsync(m => m.Id == id).Result;

            Items =_context.Item.Include(a => a.Warehouse)
                .Where(m => m.ProductId == id)
                .ToListAsync().Result;
            Ware = new HashSet<Warehouse>();

            foreach (var item in Items)
            {
                //Warehouse www = _context.Warehouse.FirstOrDefaultAsync(m => m.Id == item.Warehouse.Id).Result;
                //Ware.Add(www);


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
                await RemoveAsync(id);
                _context.Product.Remove(Product);
                await _context.SaveChangesAsync();
            }
            _notyfService.Success("Product Deleted Successfully",5);
            return RedirectToPage("./Index");
        }

        public async Task RemoveAsync(int? id)
        {
            int ProductVolume = Product.Length * Product.Width * Product.Height;
            Items = _context.Item.Include(a => a.Warehouse)
                .Where(m => m.ProductId == id)
                .ToList();
            for (int i = 0; i < Items.Count(); i++)
            {
                Items[i].Warehouse.Volume = Items[i].Warehouse.Volume + ProductVolume;
                _context.Attach(Items[i]).State = EntityState.Modified;
            }
        }
    }
}
