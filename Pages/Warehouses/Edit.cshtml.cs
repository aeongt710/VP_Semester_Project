using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Warehouses
{
    public class EditModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public EditModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Warehouse Warehouse { get; set; }

        [BindProperty]
        public IList<Item> AllItems { get; set; }

        [BindProperty]
        public IList<Item> ItemsInWareHouse { get; set; }

        public IList<Product> AllProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == id);
            AllItems = await _context.Item.ToListAsync();
            AllProducts = await _context.Product.ToListAsync();
            ItemsInWareHouse = AllItems.Where(m => m.WarehouseId == Warehouse.Id).ToList();

            if (Warehouse == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(String TaskOf, int PID, int WID,  int IID)
        {
            if (TaskOf == null) { 
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Attach(Warehouse).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(Warehouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("./Index");
            }
            else if(TaskOf == "AddProduct")
            {
                if(AddProduct(PID, WID))
                {
                    //Notification
                }
            }
            else if(TaskOf == "RemoveItem")
            {
                RemoveItem(IID, WID);
            }

            return RedirectToAction("", new { id = WID });
        }
        private void RemoveItem(int iid,int wid)
        {
            int pid=_context.Item.FirstOrDefault(m => m.Id == iid).ProductId;
            Product p = _context.Product.FirstOrDefault(m => m.Id == pid);
            int vol = p.Length * p.Width * p.Height;
            Warehouse W = _context.Warehouse.FirstOrDefault(m => m.Id == wid);
            W.Volume = W.Volume + vol;
            _context.Item.Remove(_context.Item.FirstOrDefault(m =>m.Id==iid));
            _context.Attach(W).State = EntityState.Modified;
            _context.SaveChanges();
        }
        private bool AddProduct(int pid,int wid)
        {
            Product p = _context.Product.FirstOrDefault(m => m.Id == pid);
            int vol = p.Length * p.Width * p.Height;
            Warehouse W = _context.Warehouse.FirstOrDefault(m => m.Id == wid);
            if(vol<= W.Volume)
            {
                _context.Item.Add(new Item { ProductId = pid, WarehouseId = wid });
                W.Volume = W.Volume - vol;
                _context.Attach(W).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouse.Any(e => e.Id == id);
        }


    }
}
