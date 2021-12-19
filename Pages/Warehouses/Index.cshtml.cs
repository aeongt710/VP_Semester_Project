using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Warehouses
{

    public class IndexModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public IndexModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Warehouse> Warehouse { get;set; }

        //public async Task OnGetAsync()
        //{
        //    Warehouse = await _context.Warehouse.ToListAsync();
        //}


        public async Task OnGetAsync(int id,string WarehouseName,string TaskOf, int MinVolume)
        {
            AllItems = await _context.Item.ToListAsync();
            Warehouse = await _context.Warehouse.ToListAsync();
            AllProducts = await _context.Product.ToListAsync();
            if (WarehouseName != null && TaskOf == "SearchWarehouseName")
                Warehouse = Warehouse.Where(m => m.Name == WarehouseName).ToList();
            else if (TaskOf == "SearchWarehouseID")
            {
                Warehouse = Warehouse.Where(m => m.Id == id).ToList();
            }
            else if (TaskOf == "SearchWarehouseMinVolume")
            {
                Warehouse = Warehouse.Where(m => m.Volume >= MinVolume).ToList();
            }
                
        }
        [BindProperty]
        public int MinVolume { get; set; }

        public int Volume { get; set; }
        [BindProperty]
        public IList<Item> AllItems { get; set; }
        [BindProperty]
        public IList<Item> ItemsInWarehouse { get; set; }
        //Bind removed, as it results in invalid ModelState
        //[BindProperty]   
        public Product Product { get; set; }
        public IList<Product> AllProducts { get; set; }
        [BindProperty]
        public string WarehouseName { get; set; }
        public async Task<IActionResult> OnPostAsync(string WarehouseName, string taskof, int MinVolume)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToAction("", new {WarehouseName = WarehouseName, TaskOf = taskof, MinVolume = MinVolume });

        }
    }
}
