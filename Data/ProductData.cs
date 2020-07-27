using Microsoft.EntityFrameworkCore;
using StorePriceComparison.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Data
{
    public class ProductData : IProductData
    {
        private readonly StorePriceComparisonDbContext db;

        public ProductData(StorePriceComparisonDbContext db)
        {
            this.db = db;
        }
        public IQueryable<Product> GetProductList()
        {
            var products = db.Products
                          .Include(p => p.Category)
                          .Include(p => p.Prices)
                          .ThenInclude(p => p.Store)
                          .AsNoTracking();
            return products;
        }
    }
}
