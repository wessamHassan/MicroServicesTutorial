using ECommerce.Api.Customers.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;
        private readonly ILogger<CustomersController> logger;

        public CustomersController(ICustomersProvider customersProvider, ILogger<CustomersController> logger)
        {
            this.customersProvider = customersProvider;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            logger.LogWarning("it's a warning in controller");
            if (result.IsSucc)
                return Ok(result.Customer);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersAsync(int id)
        {
            var result = await customersProvider.GetCustomersAsync(id);
            if (result.IsSucc)
                return Ok(result.Customer);
            return NotFound();
        }
    }
}
