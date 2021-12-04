using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sem1.Models
{
    public class Warehouse
    {
        [Display(Name = "Warehouse ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Volume")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter correct value")]
        public int Volume { get; set; }

    }
}
