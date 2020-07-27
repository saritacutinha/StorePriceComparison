using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StorePriceComparison.Data;
using StorePriceComparison.Models;


namespace StorePriceComparison.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductData db_ProductData;
        private readonly IPriceData db_PriceData;


        public HomeController(ILogger<HomeController> logger, IProductData db, IPriceData dbForPriceData)
        {
            _logger = logger;
            this.db_ProductData = db;
            this.db_PriceData = dbForPriceData;
        }         
        
         
       public IActionResult Index(string searchString=null)
        {
            var products = db_ProductData.GetProductList();   
            
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
              
                products.ToList();
            }
            return View(products);     
         
        }


        [HttpPost]
        public void AddNewPrice(string productId, string storeId, string price)
        {
            /* Adds the price update to the Db.*/
             
            int product = Convert.ToInt32(productId);
            int store = Convert.ToInt32(storeId);
            int productPrice = Convert.ToInt32(price);

            db_PriceData.UpdatePriceValue(product, store, productPrice);
           
            
        }
        //public async Task AddNewPriceAsync(string productid, string storeid, string price)
        //{

        //    int productId = Convert.ToInt32(productid);
        //    int storeId = Convert.ToInt32(storeid);
        //    int priceAmount = Convert.ToInt32(price);

           
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //    }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
