using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StorePriceComparison.Data;
using StorePriceComparison.Models;

namespace StorePriceComparison.Controllers
{
    public class PriceController : Controller
    {
        private readonly StorePriceComparisonDbContext db;
        public PriceController(StorePriceComparisonDbContext db)
        {
            this.db = db;
        }
       public async Task<IActionResult>Index()
        {
            var prices =  db.Prices
                               .Include(p => p.Product)
                               .Include(s => s.Store)
                               .AsNoTracking();
            return View(await prices.ToListAsync());                           
        }


        public IActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("StoreID","ProductID","Quantity","Amount")] Price price)
        {
            if(ModelState.IsValid)
            {
                db.Add(price);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateDropDownList(price.ProductID, price.StoreID);
            return View(price);
        }
        private void PopulateDropDownList(object selectedProduct = null,object selectedStore=null)
        {
           
            var productQuery = from p in db.Products
                                orderby p.Name
                                select p;
            ViewBag.ProductID = new SelectList(productQuery.AsNoTracking(), "ProductID", "Name", selectedProduct);
            var storeQuery = from s in db.Stores
                               orderby s.Name
                               select s;
            ViewBag.StoreID = new SelectList(storeQuery.AsNoTracking(), "StoreID", "Name", selectedStore);
        }
       


    }
}