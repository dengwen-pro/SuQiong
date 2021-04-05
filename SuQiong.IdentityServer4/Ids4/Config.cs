using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuQiong.IdentityServer4.Ids4
{
    public class Config
    {

        public static IEnumerable<IdentityResource> IdentityResourceResources
        {
            get
            {
                return new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                };
            }
        }

        public static List<ApiScope> ApiScopes
        {
            get
            {
                return new List<ApiScope>()
                {
                     new ApiScope("suqiongApi", "suqiongApi")
                };
            }
        }

        public static List<Client> Clients => new List<Client>()
        {
            new Client {
                ClientId = "suqiong.api.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("suqiong.api.client.secret".Sha256())
                },
                AccessTokenLifetime = 30 * 24 * 3600,   //设置access_token过期时间
                AccessTokenType = AccessTokenType.Jwt,
                RefreshTokenUsage = TokenUsage.ReUse,
                AllowOfflineAccess = true,             //如果要获取refresh_token ,必须把AllowOfflineAccess设置为true
                AllowedScopes = {
                    "suqiongApi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="suqiong",
                    Password="suqiong123",
                    Claims = new List<Claim>(){
                        new Claim(JwtRegisteredClaimNames.Aud,"suqiongApi")
                    }
                }
            };
        }

    }
}
