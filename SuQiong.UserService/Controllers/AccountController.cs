using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SuQiong.Domain.Service;
using SuQiong.EntityFrameworkCore.Context;
using SuQiong.EntityFrameworkCore.Model;
using SuQiong.Domain.Models.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuQiong.UserService.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Ids4Option _ids4Option;

        public AccountController(IUserService userService,
            IOptions<Ids4Option> ids4Option)
        {
            _userService = userService;
            _ids4Option = ids4Option.Value;
        }

        [Route("v1/account")]
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = _ids4Option.Address,
                Policy = new DiscoveryPolicy()
                {
                    RequireHttps = false
                }
            });

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                UserName = _ids4Option.UserName,
                Password = _ids4Option.Password,
                ClientId = _ids4Option.ClientId,
                ClientSecret = _ids4Option.ClientSecret,
                Scope = _ids4Option.Scope,
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            return Ok(tokenResponse.Raw);
        }

        //[Authorize]
        [Route("v1/users")]
        public async Task<IActionResult> GetUserInfo()
        {
            //for (int i = 2; i <= 11; i++)
            //{
            //    await _userService.Insert(new User()
            //    {
            //        UserId = i,
            //        UserName = "dengwen" + i,
            //        Password = "123456"
            //    }); ;
            //}
            var users = await _userService.Select(10, 2, x => true, x => x.UserId, true);
            return await Task.FromResult(Ok(users));
        }

        [Route("v1/goods")]
        public async Task<string> GetGoods()
        {
            var httpClient = new HttpClient();
            using (var consul = new Consul.ConsulClient(c =>
            {
                c.Address = new Uri("http://192.168.1.104:8500");
            }))
            {
                var services = consul.Agent.Services().Result.Response.Values.Where(p => p.Service.Equals("product-service", StringComparison.OrdinalIgnoreCase)); ;
                Random rand = new Random();
                var index = rand.Next(services.Count());
                var s = services.ElementAt(index);
                return await httpClient.GetStringAsync($"http://{s.Address}:{s.Port}/healthCheck");
            }

        }
    }
}
