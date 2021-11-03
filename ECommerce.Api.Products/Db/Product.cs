using ECommerce.Api.Products.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Db
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Inventory { get; set; }

        public TestEnum TestingEnum { get; set; }
    }
}
