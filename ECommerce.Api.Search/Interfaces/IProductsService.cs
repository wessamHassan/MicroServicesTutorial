using ECommerce.Api.Search.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSucc, IEnumerable<Product> Products, string ErrorMsg)> GetProductsAsync();
    }
}
