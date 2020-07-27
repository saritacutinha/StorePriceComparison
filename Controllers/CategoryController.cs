using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorePriceComparison.Data;
using StorePriceComparison.Models;

namespace StorePriceComparison.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StorePriceComparisonDbContext db;
        public CategoryController(StorePriceComparisonDbContext db)
        {
            this.db = db;
        }
        
        public async Task<IActionResult>Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" :"";
            
            var categories = from c in db.Categories
                               select c;
            if (searchString != null)
                pageNumber = 1;
            //categories = categories.Where(c => c.Name.Contains(searchString));
            else
                currentFilter = searchString;
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
                categories = categories.Where(c => c.Name.Contains(searchString));

            switch (sortOrder)
            {
                case "name_desc":
                   categories =  categories.OrderByDescending(c => c.Name);                   
                    break;
                default:
                    categories = categories.OrderBy(c => c.Name);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Category>.CreateAsync(categories.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult>Details(int? id)
        {
            if (id == null)
                return View("NotFound");
            var category = await db.Categories
                .Include(c => c.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryID == id);
            if (category == null)
                return View("NotFound");
            return View(category);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if(String.IsNullOrEmpty(category.Name))
        //    {
        //        ModelState.AddModelError(nameof(category.Name), "The name is required");
        //    }
        //    if(ModelState.IsValid)
        //    {
        //        db.Add(category);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
       
        ////public IActionResult Details(int id)
        ////{
        ////    var model = db.GetById(id);
        ////    if(model ==null)
        ////    {
        ////        return View("Not Found");
        ////    }
        ////    return View(model);
        ////}

        ////[HttpGet]
        ////public IActionResult Edit(int id)
        ////{
        ////    var model = db.GetById(id);
        ////    if(model == null)
        ////    {
        ////        return View("NotFound");
        ////    }
        ////    return View(model);            
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryTokenAttribute]
        ////public IActionResult Edit(Category category)
        ////{
        ////    if(String.IsNullOrEmpty(category.Name))
        ////    {
        ////        ModelState.AddModelError(category.Name, "The name is required");
                                
        ////    }
        ////    if(ModelState.IsValid)
        ////    {
        ////        db.Update(category);
        ////        TempData["Message"] = "You have saved the Category!";
        ////        return RedirectToAction("Index");
                
        ////    }
        ////    return View();
        ////}
        ////[HttpGet]
        ////public IActionResult Delete(int id)
        ////{
        ////    var model = db.GetById(id);
        ////    if(model == null)
        ////    {
        ////        return View("NotFound");
        ////     }
        ////    return View(model);
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public IActionResult Delete(int id, Microsoft.AspNetCore.Http.IFormCollection form)
        ////{
        ////    db.Delete(id);
        ////    return  RedirectToAction("Index");
        ////}



    }
}