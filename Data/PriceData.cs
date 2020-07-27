using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Data
{
    public class PriceData : IPriceData
    {

        private readonly StorePriceComparisonDbContext db;

        public PriceData(StorePriceComparisonDbContext db)
        {
            this.db = db;
        }
        public void UpdatePriceValue(int product, int store, float productPrice)
        {
            var priceValueStored =db.Prices.FirstOrDefault(p => p.ProductID == product && p.StoreID == store);
            if (priceValueStored != null)
            {
                priceValueStored.Amount = productPrice;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                Console.WriteLine("Price Value could not be updated");
            }

        }    
    }
}
