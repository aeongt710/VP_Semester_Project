using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly sem1.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, sem1.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public int _items { get; set; }

        public int _warehouse { get; set; }

        public int _products { get; set; }
        public void OnGet()
        {
            _items = _context.Item.ToList().Count();
            _warehouse = _context.Warehouse.ToList().Count();
            _products = _context.Product.ToList().Count();
        }
    }
}
