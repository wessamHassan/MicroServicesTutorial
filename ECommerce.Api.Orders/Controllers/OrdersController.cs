﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProviders ordersProviders;

        public OrdersController(IOrdersProviders ordersProviders)
        {
            this.ordersProviders = ordersProviders;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProviders.GetOrdersAsync(customerId);
            if (result.IsSucc)
                return Ok(result.orders);
            return NotFound();
        }
    }
}