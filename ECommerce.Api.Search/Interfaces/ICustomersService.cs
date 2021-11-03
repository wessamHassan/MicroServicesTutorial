using ECommerce.Api.Search.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSucc, Customer Customers, string ErrorMsg)> GetCustomerAsync(int id);
    }
}
