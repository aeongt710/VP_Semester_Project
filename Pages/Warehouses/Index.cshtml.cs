using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
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


        public async Task OnGetAsync(int id,string WarehouseName,string TaskOf, int MinVolume,int MaxVolume)
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
            else if (TaskOf == "FileterWarehouseByVolume")
            {
                Warehouse = Warehouse.Where(m => m.Volume >= MinVolume && m.Volume <= MaxVolume).ToList();
            }
        }

        [Display(Name = "Minimum Available Volume")]
        [BindProperty]
        public int MinVolume { get; set; }
        [Display(Name = "Maximum Available Volume")]
        [BindProperty]
        public int MaxVolume { get; set; }
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
        public async Task<IActionResult> OnPostAsync(string WarehouseName, string taskof, int MinVolume,int MaxVolume)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToAction("", new {WarehouseName = WarehouseName, TaskOf = taskof, MinVolume = MinVolume, MaxVolume = MaxVolume });

        }
    }
}
