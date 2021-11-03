using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ICustomersService> logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<ICustomersService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSucc, Customer Customers, string ErrorMsg)> GetCustomerAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                // Don't forget that, this "api/customers/{id}" return only one customer
                // so we return here Customer not IEnumrable<Customers>

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Customer>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
