using ECommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    /*
     * it is an interface between Controller and Model
     */
    public interface IProductsProvider
    {
        /*
         * to get all products (from model)
         * returns:
         * IsSucc 
         * Products
         * ErrorMsg if exist
         */
        Task<(bool IsSucc, IEnumerable<Product> Products, string ErrorMsg)> GetProductsAsync();

        Task<(bool IsSucc, Product Product, string ErrorMsg)> GetProductsAsync(int id);
        Task<(bool IsSucc, string ErrorMsg)> AddProductAsync(Models.Product product);
        Task<(bool IsSucc, string ErrorMsg)> UpdateProductAsync(int id, Models.Product product);
    }
}
