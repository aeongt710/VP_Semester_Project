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

namespace sem1.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public IndexModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [Display(Name="Warehouse Name")]
        public string WarehouseName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [BindProperty]
        public IList<Item> Item { get;set; }

        //public async Task OnGetAsync()
        //{
        //    Item = await _context.Item
        //        .Include(i => i.Product)
        //        .Include(i => i.Warehouse).ToListAsync();
        //}

        public async Task OnGetAsync(int id, string taskof, string ProductName, string WarehouseName)
        {
            Item = await _context.Item
                .Include(i => i.Product)
                .Include(i => i.Warehouse).ToListAsync();
            if (taskof == "RouteWarehouseID")
                Item = Item.Where(m => m.WarehouseId == id).ToList();
            else if (taskof == "RouteProductID")
                Item = Item.Where(m => m.ProductId == id).ToList();
            else if (taskof == "SearchProductName")
                Item = Item.Where(m => m.Product.Name.ToLower() == ProductName.ToLower()).ToList();
            else if (taskof == "SearchWarehouseName")
                Item = Item.Where(m => m.Warehouse.Name.ToLower() == WarehouseName.ToLower()).ToList();
        }
        //public async Task OnGetAsync(string ProductName)
        //{
        //    Item = await _context.Item
        //        .Include(i => i.Product)
        //        .Include(i => i.Warehouse).ToListAsync();

        //}

        public async Task<IActionResult> OnPostAsync(string ProductName,string WarehouseName,string taskof)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            return RedirectToAction("", new
            {
                ProductName = ProductName, WarehouseName= WarehouseName, taskof = taskof
            });

        }
    }
}
