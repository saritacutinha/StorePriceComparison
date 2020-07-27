using StorePriceComparison.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Data
{
    public static class DBInitialiser
    {
        public static void Initialize(StorePriceComparisonDbContext db)
        {
            db.Database.EnsureCreated();
            if (db.Stores.Any())
            {
                return;   // DB has been seeded
            }
            var stores = new Store[]
            {
                new Store() {  Name = "Coles", Address = "Payneham Rd, Stepney SA 5069" },
                 new Store() { Name = "Alde", Address = "398 Magill Rd, Kensington Park SA 5068" },
                 new Store() {  Name = "Woolsworth", Address = "104 Walkerville Terrace, Walkerville SA 5081" }
            };
            foreach (Store s in stores)
            {
                db.Stores.Add(s);
            }
            db.SaveChanges();
            Console.WriteLine("Store data seeded");

            //Seeding Category Data

            var categories = new Category[]
            {
                 new Category { Name = "Baby Items" },
                 new Category { Name = "Baking" },
                 new Category { Name = "Beverages" },
                 new Category { Name = "Bread/Bakery" },
                 new Category { Name = "Bulk" },
                 new Category { Name = "Canned Goods" },
                 new Category { Name = "Cereal/Breakfast" },
                 new Category {Name = "Condiments" },
                 new Category { Name = "Dairy" },
                 new Category {  Name = "Deli" },
                 new Category { Name = "Frozen" },
                 new Category { Name = "Meats" },
                 new Category { Name = "Pasta/Rice" },
                 new Category { Name = "Produce" },
                 new Category { Name = "Snacks" },

                 //non-food
                 new Category { Name = "Cleaning Supplies" },
                 new Category { Name = "Health and Beauty" },
                 new Category { Name = "Kitchen Utensils" },
                 new Category { Name = "Paper Products" },
                 new Category { Name = "Pet Supplies" },

                 //finally
                 new Category { Name = "Other" }
            };
            foreach (Category c in categories)
            {
                db.Categories.Add(c);
            }
            db.SaveChanges();


            //Seeding Product Data
            var products = new Product[]
            {

                new Product
                   {
                     Name = "Great Grains Crunchy Pecans", CategoryID=categories.Single( p =>p.Name =="Pet Supplies").CategoryID
                   },

            };
            foreach (Product p in products)
            {
                db.Products.Add(p);
            }
            db.SaveChanges();

            var prices = new Price[]
            {
                new Price
                {
                   StoreID= stores.Single(s =>s.Name =="Coles").StoreID,
                    ProductID = products.Single(p =>p.Name =="Great Grains Crunchy Pecans").ProductID,
                    Quantity=Quantity.Pound,Amount=10
                }
            };
            foreach (Price p in prices)
            {
                db.Prices.Add(p);
            }
            db.SaveChanges();
        }
    }
}
