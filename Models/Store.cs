using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Models
{
    public class Store
    {
               
        public int StoreID { get; set; }

        [Required, Display(Name = "Store Name"), StringLength(50, MinimumLength = 1)]
        public String Name { get; set; }   
        public String Address { get; set; }
        public ICollection<Price> Prices { get; set; }
    }
}
