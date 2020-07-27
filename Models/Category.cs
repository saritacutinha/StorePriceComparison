using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Models
{
    public class Category
    {
       
        public int CategoryID { get; set; }

        [Required, Display(Name = "Category Name"), StringLength(50, MinimumLength = 1)]
        public String Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
