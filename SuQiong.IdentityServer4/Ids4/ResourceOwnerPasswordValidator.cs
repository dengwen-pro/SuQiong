using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using SuQiong.EntityFrameworkCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuQiong.IdentityServer4.Ids4
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private readonly SqMallDbContext _context;

        public ResourceOwnerPasswordValidator(SqMallDbContext context)
        {
            _context = context;
        }

        public ResourceOwnerPasswordValidator()
        {

        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == context.UserName && x.Password == context.Password);
            if (user is null)
            {
                context.Result = new GrantValidationResult(new Dictionary<string, object> {
                    { "invalid_grant", "登录失败" }
                });
                return;
            }

            context.Result = new GrantValidationResult(
                subject: context.UserName,
                authenticationMethod: OidcConstants.AuthenticationMethods.Password);
            //claims: new Claim[] {
            //        new Claim("user_id" , user.ID.ToString())
            //});
        }

        //public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        //{
        //    var result = await _accountManager.LoginAsync(context.UserName, context.Password);
        //    int user_id = result.Key;
        //    if (user_id > 0)
        //    {
        //        var user = await _accountManager.QueryUserInfoAsync(user_id);

        //        context.Result = new GrantValidationResult(
        //            subject: context.UserName,
        //            authenticationMethod: OidcConstants.AuthenticationMethods.Password,
        //            claims: new Claim[] {
        //                new Claim("user_id" , user.ID.ToString())
        //            });
        //    }
        //    else
        //    {
        //        //验证失败
        //        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, result.Value);
        //    }
        //}
    }
}
