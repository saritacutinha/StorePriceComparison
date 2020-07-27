using StorePriceComparison.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Data
{
    public interface IProductData
    {      

        public IQueryable<Product> GetProductList();
        
    }
}
