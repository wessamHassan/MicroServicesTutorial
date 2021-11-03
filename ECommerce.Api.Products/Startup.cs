using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;


namespace ECommerce.Api.Products
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //[Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             * 2] to consider which implentation of this interface
             */
            services.AddScoped<IProductsProvider, ProductsProvider>();

            //services.AddMvc(option => option.EnableEndpointRouting = false);
                
                
            //services.AddSingleton<IProductsProvider, ProductsProvider>();
           // services.AddTransient<IProductsProvider, ProductsProvider>();

            /*
             * 3] ?? auto mapper something like model which displayes only needed properties in data model
             */
            services.AddAutoMapper(typeof(Startup));


            /* 1]
             * it defines the DB which will we use
             * here we will use in memory DB
             * so he made general constructor in ProductsDbContext as:
             * public ProductsDbContext(DbContextOptions options):base(options)
             * and here he defined this option parameter using lamda expression
             
             * AND IF I HAVE A SQL SERVER DB:
             * services.AddDbContext<ProductsDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("xyz")));
             */
            services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products");
            });

            //services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyDBConnection")));

            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    //s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //used to specify how the app responds to HTTP requests.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Api v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
