using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem1.Data;
using sem1.Models;

namespace sem1.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly sem1.Data.ApplicationDbContext _context;

        public DetailsModel(sem1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
