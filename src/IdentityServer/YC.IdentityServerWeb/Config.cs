using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace YC.IdentityServerWeb
{
    /// <summary>
    /// SeedData 中调用
    /// </summary>
    public static class Config
    {
        // API Scope
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            // Claim
            new ApiScope
            {
                Name = "test_api",
                DisplayName = "test API",
            },
        };
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
            {
                new ApiResource("test_api")
                {
                    ApiSecrets = {new Secret("secret".Sha256())} ,
                    //Scopes = { new Scope("test_api") }

                }
            };


        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "test_client",
                ClientSecrets =
                {
                    new Secret("test123".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"test_api"},
                AllowOfflineAccess=true,
            },
            new Client
            {
                
                ClientId = "test_pass_client",
                ClientSecrets =
                {
                    new Secret("test123".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = {"test_api"},
                  AllowOfflineAccess=true,//允许刷新token
                 AccessTokenLifetime=7200//token 过期时间
                
            },
            new Client
            {
                ClientId = "test_mvc_client",
                ClientName = "test MVC Client",
                ClientSecrets =
                {
                    new Secret("test123".Sha256())
                },
                  AllowOfflineAccess=true,
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:4001/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:4001/sigout-callback-oidc"},//退出登录后跳转地址
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                RequireConsent = true//是否授权 用户操作选择
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}