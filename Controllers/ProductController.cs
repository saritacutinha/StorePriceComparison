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
    public class ProductController : Controller
    {
        private readonly StorePriceComparisonDbContext db;
        public ProductController(StorePriceComparisonDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var products = db.Products
                .Include(c => c.Category)
                .AsNoTracking();
            return View(await products.ToListAsync());
        }

        public IActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategoryDropDownList(product.CategoryID);
            return View(product);
        }

        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult>Edit(int? id)
        {
            
            if (id == null)
                return NotFound();            
            var product = await db.Products
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
                return NotFound();
            PopulateCategoryDropDownList(product.CategoryID);
            return View(product);        
        }
               
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditPost(int? id)
        {
            if (id == null)
                return NotFound();
            //Find the product to update
            var productToUpdate = await db.Products
                                   
                                    .FirstOrDefaultAsync(p => p.ProductID == id);
            if (productToUpdate == null)
                return NotFound();
            if(await TryUpdateModelAsync<Product>(productToUpdate,
                "",
                p => p.Name,p => p.CategoryID))
            {
                try
                {
                    await db.SaveChangesAsync();
                }
                catch(DbUpdateException /*ex*/)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists, " +
                "see your system administrator.");
                }
                PopulateCategoryDropDownList(productToUpdate);
                return RedirectToAction(nameof(Index));   
            }
            return View(productToUpdate);

        }

        public async Task<IActionResult>Details(int?id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products
                                .Include(c => c.Category)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }
            PopulateCategoryDropDownList(product.CategoryID);
            return View(product);
        }

        public async Task<IActionResult>Delete(int?id)
        {
            if (id == null)
                return NotFound();
            var product = await db.Products
                                .Include(p => p.Category)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.ProductID == id);
            if(product ==null)
            {
                return NotFound();
            }
            PopulateCategoryDropDownList(product.CategoryID);
            return View(product);

        }




        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from c in db.Categories
                                   orderby c.Name
                                   select c;
            ViewBag.CategoryID = new SelectList(categoryQuery.AsNoTracking(),"CategoryID", "Name", selectedCategory);
        }
    }
}