using StorePriceComparison.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StorePriceComparison.Data
{
    public class StorePriceComparisonDbContext : DbContext
    {
        public StorePriceComparisonDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            modelbuilder.Entity<Store>().ToTable("Store");
            modelbuilder.Entity<Category>().ToTable("Category");
            modelbuilder.Entity<Product>().ToTable("Product");
            modelbuilder.Entity<Price>().ToTable("Price");
        }                


    }
}
