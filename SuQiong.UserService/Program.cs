using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winton.Extensions.Configuration.Consul;

namespace SuQiong.UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                        //使用Consul配置中心,动态获取应用配置
                        hostingContext.Configuration = config.Build();
                        //var argsDic = new Dictionary<string, string>();
                        //for (int i = 0; i < args.Length; i += 2)
                        //    argsDic.Add(args[i], args[i + 1]);
                        //string consulConfigName = argsDic.TryGetValue("--consulConfigName", out string value) ? value : hostingContext.Configuration["ConsulConfigName"];
                        //Console.WriteLine("consulConfigName:" + consulConfigName);
                        string consulUrl = hostingContext.Configuration["ConsulUrl"];
                        string consulConfigName = hostingContext.Configuration["ConsulConfigName"];
                        Console.WriteLine(string.Concat("consulUrl:", consulUrl, "\n", "consulConfigName:", consulConfigName));
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
