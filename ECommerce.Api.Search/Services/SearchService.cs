using ECommerce.Api.Search.Interfaces;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService orderService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService orderService, IProductsService productsService,ICustomersService customersService)
        {
            this.orderService = orderService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSucc, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsync();
            var customerResult = await customersService.GetCustomerAsync(customerId);

            if (orderResult.IsSucc)
            {
                foreach(var order in orderResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productResult.IsSucc?
                            productResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name : "Product Name is not available";
                    }
                }
                var result = new
                {
                    CustomerName = customerResult.IsSucc ? customerResult.Customers.Name : "Customer Name is not available",// : new { Name = "Customer Name is not available" },
                    MyOrders = orderResult.Orders
                };
                return (true, result);
                
                //return (true, orderResult.Orders);

            }

            return (false, null);
        }
    }
}
