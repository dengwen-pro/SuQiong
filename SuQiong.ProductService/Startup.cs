using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SuQiong.Application.Extensions;
using SuQiong.EntityFrameworkCore.Context;
using SuQiong.Domain.Models.Options;

namespace SuQiong.ProductService
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
            #region EF Core 
            services.AddDbContext<SqMallDbContext>(d => d.UseMySql(Configuration.GetValue<string>("ConntectionString")
                , new MySqlServerVersion(new System.Version(8, 0, 22))
                , mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
            #endregion

            #region IdentityServer4
            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["Ids4Option:Address"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    options.Audience = Configuration["Ids4Option:Scope"];
                });
            #endregion

            #region Dependency injection
            services.AddOptions()
                .Configure<Ids4Option>(Configuration.GetSection(nameof(Ids4Option)));
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();    //认证服务
            app.UseAuthorization();     //使用授权服务

            //服务注册
            var consulOption = new ConsulOption();
            Configuration.Bind(nameof(ConsulOption), consulOption);
            app.UseConusl(hostApplicationLifetime, consulOption);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
