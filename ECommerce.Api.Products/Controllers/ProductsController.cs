using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ServiceReference1.NumberConversionSoapTypeClient;
//using Calculator;
//using static Calculator.CalculatorSoapClient;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            //int num1 = 10, num2 = 0;
            //CalculatorSoapClient calc = new CalculatorSoapClient(EndpointConfiguration.CalculatorSoap);

            //return Ok(await calc.SubtractAsync(num1, num2));

            //int num = 1;
            //NumberConversionSoapTypeClient client = new NumberConversionSoapTypeClient(EndpointConfiguration.NumberConversionSoap);
            //var result = await client.NumberToWordsAsync(((ulong)num));

            // return Ok(result.Body.NumberToWordsResult);
            try
            {
                var result = await productsProvider.GetProductsAsync();
                //Exception x = new Exception();
                //throw x;
                if (result.IsSucc)
                    return Ok(result.Products);
                return NotFound();
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failuer");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsAsync(int id)
        {
            var result = await productsProvider.GetProductsAsync(id);
            if (result.IsSucc)
                return Ok(result.Product);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            var result = await productsProvider.AddProductAsync(product);
            if (result.IsSucc)
                return StatusCode(StatusCodes.Status201Created);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            var result = await productsProvider.UpdateProductAsync(id, product);
            if (result.IsSucc)
                return StatusCode(StatusCodes.Status201Created);
            return NotFound();
        }

        //[HttpGet("{num}")]
        //public  IActionResult GetStringToNum(int num)
        //{
        //    NumberToWordsRequestBody requestBody = new NumberToWordsRequestBody(((ulong)num));
        //    NumberToWordsRequest request = new NumberToWordsRequest(requestBody);

        //    NumberToWordsResponse 

        //        return Ok(result);

        //}

        //[HttpGet]
        //public IActionResult GetStringToNum()
        //{
        //    int num1 = 1, num2 = 2;
        //    CalculatorSoapClient calc = new CalculatorSoapClient(EndpointConfiguration.CalculatorSoap);

        //    return Ok(calc.AddAsync(num1, num2));

        //}
    }
}
