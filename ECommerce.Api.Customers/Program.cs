using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;

namespace ECommerce.Api.Customers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                 .MinimumLevel.Warning()
                 .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: "NewBookStoreDB", sinkOptions: new MSSqlServerSinkOptions { TableName = "Logx" }
                    , null, configuration, LogEventLevel.Information, null, null, null).CreateLogger();

                    //tableName: "Log",
                    //appConfiguration: configuration,
                    //autoCreateSqlTable: true)
                      //  .CreateLogger();
                
          
           // .WriteTo.MSSqlServer(
           //         connectionString: "NewBookStoreDB",
           //         tableName: "Log",
           //         appConfiguration: configuration,
           //         autoCreateSqlTable: true)
           //.WriteTo.MSSqlServer("", "Logs", autoCreateSqlTable: true)
           //.WriteTo.MSSqlServer("NewBookStoreDB", sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }
           //    , null, null, LogEventLevel.Warning, null, null, null, null)
           //.WriteTo.Debug(new RenderedCompactJsonFormatter())
           // .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)            
           //.CreateLogger();

            try
            {
                Log.Warning("this is warning Starting Web Host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
