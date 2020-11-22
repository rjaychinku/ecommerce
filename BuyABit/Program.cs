using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuyABit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile("appsettings.Development.json")
                                .AddJsonFile("appsettings.Prod.json")
                                .AddEnvironmentVariables()
                                .Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
