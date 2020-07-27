using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePriceComparison.Models;
using Microsoft.AspNetCore.Mvc;
using StorePriceComparison.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace StorePriceComparison.Controllers
{
    

    public class StoreController : Controller
    {
        private readonly StorePriceComparisonDbContext db;

        public StoreController(StorePriceComparisonDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult>Index()
        {
            var stores =  db.Stores;
            return View(await stores.ToListAsync());                         
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("StoreID,Name,Address")]Store store)
        {
            if(ModelState.IsValid)
            {
                db.Add(store);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(store);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var store = await db.Stores
                                    .AsNoTracking()
                                   .FirstOrDefaultAsync(s => s.StoreID == id);
            if (store == null)
                return NotFound();
            return View(store);
        }

        public async Task<IActionResult>Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var store = await db.Stores
                           .AsNoTracking()
                           .FirstOrDefaultAsync(s => s.StoreID == id);
            if (store == null)
                return NotFound();
            return View(store);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditPost(int?id)
        {
            if(id ==null)
            {
                return NotFound();
            }
            var storeToUpdate = await db.Stores
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(s => s.StoreID == id);
            if(storeToUpdate == null)
                return NotFound();
            if(await TryUpdateModelAsync<Store>(storeToUpdate,"",
                s =>s.StoreID, s=> s.Name, s =>s.Address))
            {
                try
                {
                   await  db.SaveChangesAsync();
                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists, " +
                "see your system administrator.");
                }
                return RedirectToAction("Index");
            }
            return View(storeToUpdate);
        }


        public async Task<IActionResult>Delete(int?id)
        {
            if (id == null)
                return NotFound();
            var storeToDelete = await db.Stores
                                    .AsNoTracking()
                                    .SingleAsync(s => s.StoreID == id);
            if (storeToDelete == null)
                return NotFound();
            return View(storeToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var storeToDelete = await db.Stores
                                       .AsNoTracking()
                                       .SingleAsync(s => s.StoreID == id);
            if (storeToDelete == null)
                return NotFound();
            db.Stores.Remove(storeToDelete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            

        }

        //private readonly IStoreData db;
        //public StoreController(IStoreData db)
        //{
        //    this.db = db;
        //}


        //public IActionResult Index()
        //{
        //    var model = db.GetAllStores();
        //    return View(model);
        //}

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Store store)
        //{
        //    if(String.IsNullOrEmpty(store.Name))
        //    {
        //        ModelState.AddModelError(nameof(store.Name), "The name is required");
        //    }
        //    if(ModelState.IsValid)
        //    {
        //        db.Add(store);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public ActionResult Details(int id)
        //{
        //    var model = db.GetById(id);
        //    if(model ==null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var model = db.GetById(id);
        //    if (model == null)
        //        return View("NotFound");
        //    else
        //        return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Store store)
        //{
        //    if (String.IsNullOrEmpty(store.Name))
        //    {
        //        ModelState.AddModelError(nameof(store.Name), "The name is required");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        db.Update(store);
        //        TempData["Message"] = "You have saved the restaurant!";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var model = db.GetById(id);
        //    if (model == null)
        //        return View("NotFound");        

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection form)
        //{
        //    db.Delete(id);
        //    return RedirectToAction("Index");
        //}

    }
}