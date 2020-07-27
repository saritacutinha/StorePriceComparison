using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Models
{
    public enum Quantity
    {
        Each, Pound, Ounce, Fluid_Ounce, Liter, Gallon
    }
    public class Price
    {
        public int PriceID { get; set; }
        public int StoreID { get; set; }
        public int ProductID { get; set; }
        public Quantity? Quantity { get; set; }
        public float Amount{get; set; }

        public Store Store { get; set; }
        public Product Product { get; set; }
    }
}
