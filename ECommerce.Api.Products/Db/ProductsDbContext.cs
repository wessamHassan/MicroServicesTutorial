using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        //public ProductsDbContext(DbContextOptions options) : base(options)
        //{

        //}

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("Product");
           
        }
    }
}
