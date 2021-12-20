using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Models
{
    public class Item
    {
        [Display(Name = "Item ID")]
        public int Id { get; set; }
    
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        public Product Product { get; set; }

        public int WarehouseId { get; set; }
        [Display(Name = "Warehouse")]
        public Warehouse Warehouse { get; set; }


        ////[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[Display(Name = "Expiry Date")]
        //[Required]
        //public DateTime ExpiryDate { get; set; }

        ////[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[Display(Name = "Created Date")]
        //[Required]
        //public DateTime CreatedDate { get; set; }


    }
}
