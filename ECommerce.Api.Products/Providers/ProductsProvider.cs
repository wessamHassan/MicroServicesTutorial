using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    /*Implmentation of the Intrface IProductsProvider    
     * we must inject DB to fill model object
     */
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        /*
         * didn't understand ILogger or IMapper 
         */
        public ProductsProvider(ProductsDbContext dbContext,ILogger<ProductsProvider> logger,IMapper mapper)
        {
           
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
           if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product { Id = 1, Name = "product1", Price = 10, Inventory = 100});
                dbContext.Products.Add(new Db.Product { Id = 2, Name = "product2", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product { Id = 3, Name = "product3", Price = 30, Inventory = 100 });
                dbContext.Products.Add(new Db.Product { Id = 4, Name = "product4", Price = 40, Inventory = 100 });
                dbContext.Products.Add(new Db.Product { Id = 5, Name = "product5", Price = 40, Inventory = 100 });
                dbContext.Products.Add(new Db.Product { Id = 6, Name = "   ", Price = 40, Inventory = 100 });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSucc, IEnumerable<Models.Product> Products, string ErrorMsg)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    //Both ways to mapper workes

                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    //OR
                    //var result = mapper.Map<IEnumerable<Models.Product>>(products);
                   
                    return (true, result, null);
                }
                return (false, null, "not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucc, Models.Product Product, string ErrorMsg)> GetProductsAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null )
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucc, string ErrorMsg)> AddProductAsync(Models.Product product)
        {
            try
            {
                var newProduct = /*mapper.Map<Models.Product, Db.Product>(product);*/mapper.Map<Db.Product>(product);
                //add and save changes
                dbContext.Products.Add((Db.Product)newProduct);
                await dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }

        }

        public async Task<(bool IsSucc, string ErrorMsg)> UpdateProductAsync(int id, Models.Product product)
        {
            try
            {
                var oldProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id); //DB
                if(oldProduct == null)
                {
                    return (false, "not found");
                }
                mapper.Map(product, oldProduct);

                await dbContext.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }

        }
    }
}
