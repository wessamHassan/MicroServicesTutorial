using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Search
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICustomersService, CustomersService>();
            /*
             * to communicate with Orders microServices
             * we set the name to "OrderService" 
             * and set the Uri by
             * Configuration["Services:Orders"] -> this what we add in file appsettings.jason under "Services": {"Orders" : "http://localhost:59043"}
             * 
             * */
            services.AddHttpClient("OrdersService", config => config.BaseAddress = new Uri(Configuration["Services:Orders"]));
            services.AddControllers();
            //For Products
            services.AddHttpClient("ProductsService", config => config.BaseAddress = new Uri(Configuration["Services:Products"]));
            services.AddControllers();
            //For Customers
            services.AddHttpClient("CustomersService", config => config.BaseAddress = new Uri(Configuration["Services:Customers"]));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
