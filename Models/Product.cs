using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Models
{
    public class Product
    {
        [Display(Name = "Product ID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Length")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter correct value")]
        public int Length { get; set; }

        [Display(Name = "Width")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter correct value")]
        public int Width { get; set; }

        [Display(Name = "Height")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter correct value")]
        public int Height { get; set; }


        //[Required]
        //[Display(Name = "Price")]
        //public int Price { get; set; }


        [Display(Name = "Description")]
        public String Description { get; set; }


        [Display(Name = "Product Vendor")]
        public String Vendor { get; set; }



        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }
    }
}
