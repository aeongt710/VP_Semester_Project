using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using sem1.Models;

namespace sem1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<sem1.Models.Product> Product { get; set; }
        public DbSet<sem1.Models.Item> Item { get; set; }
        public DbSet<sem1.Models.Warehouse> Warehouse { get; set; }
    }
}
