using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interface;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext customersDbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext customersDbContext,ILogger<CustomersProvider> logger,IMapper mapper)
        {
            this.customersDbContext = customersDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!customersDbContext.Customers.Any())
            {
                customersDbContext.Customers.Add(new Db.Customer { Id = 1, Name = "Customer 1", Address = "Address 1" });
                customersDbContext.Customers.Add(new Db.Customer { Id = 2, Name = "Customer 2", Address = "Address 2" });
                customersDbContext.Customers.Add(new Db.Customer { Id = 3, Name = "Customer 3", Address = "Address 3" });

                customersDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSucc, IEnumerable<Models.Customer> Customer, string ErrorMsg)> GetCustomersAsync()
        {
            try
            {
                var customers = await customersDbContext.Customers.ToListAsync();
                logger.LogInformation("calling get customer");
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                logger.LogWarning("no data found");
                return (false, null, "no data found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucc, Models.Customer Customer, string ErrorMsg)> GetCustomersAsync(int id)
        {
            try
            {
                var customer = await customersDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null )
                {
                    var result = mapper.Map<Db.Customer,Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "no data found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
