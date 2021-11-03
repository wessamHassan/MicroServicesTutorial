using ECommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProviders
    {
        Task<(bool IsSucc, IEnumerable<Order> orders, string ErrorMsg)> GetOrdersAsync(int customerId);
    }
}
