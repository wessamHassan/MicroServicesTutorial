using ECommerce.Api.Search.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSucc, IEnumerable<Order> Orders, string ErrorMsg)> GetOrdersAsync(int customerId);
    }
}
