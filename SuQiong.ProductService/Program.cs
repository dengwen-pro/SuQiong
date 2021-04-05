using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winton.Extensions.Configuration.Consul;

namespace SuQiong.ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                        //使用Consul配置中心,动态获取应用配置
                        hostingContext.Configuration = config.Build();
                        string consulUrl = hostingContext.Configuration["ConsulUrl"];
                        config.AddConsul(
                                    $"product-service/appsettings.json",
                                    options =>
                                    {
                                        options.Optional = true;
                                        options.ReloadOnChange = true;
                                        options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                                        options.ConsulConfigurationOptions = cco => { cco.Address = new Uri(consulUrl); };
                                    });

                        hostingContext.Configuration = config.Build();
                    });
                })
            ;
    }
}
