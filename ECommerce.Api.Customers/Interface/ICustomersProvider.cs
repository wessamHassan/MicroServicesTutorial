using ECommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interface
{
    public interface ICustomersProvider
    {
        Task<(bool IsSucc, IEnumerable<Customer> Customer, string ErrorMsg)> GetCustomersAsync();
        Task<(bool IsSucc, Customer Customer, string ErrorMsg)> GetCustomersAsync(int id);
    }
}
