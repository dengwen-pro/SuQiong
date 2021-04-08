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
                        //添加环境变量
                        config.AddEnvironmentVariables();

                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                        hostingContext.Configuration = config.Build();
                        string consulUrl = hostingContext.Configuration["ConsulUrl"];
                        string consulConfigName = hostingContext.Configuration["ConsulConfigName"];
                        Console.WriteLine(string.Concat("consulUrl:", consulUrl, "\n", "consulConfigName:", consulConfigName));

                        //添加Consul配置中心
                        config.AddConsul(
                                    consulConfigName,
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
