using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YC.IdentityServerWeb.Models;

namespace YC.IdentityServerWeb
{
    public static class SeedData
    {
        public static void InitIdentityServerDb(IApplicationBuilder app)
        {
            // 创建服务范围
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var persistedGrantDb = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database;

            if (persistedGrantDb.GetPendingMigrations().Any())
            {
                // 从服务容器中获取持久授权上下文对象，执行迁移
                persistedGrantDb.Migrate();
            }
            var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            var configurationDb = configurationDbContext.Database;

            if (configurationDb.GetPendingMigrations().Any())
            {
                // 从服务容器中获取持久授权上下文对象
                configurationDb.Migrate();
            }

            // 初始化客户端
            if (!configurationDbContext.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    configurationDbContext.Clients.Add(client.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            // 初始化认证资源
            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                  
                    configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            // 初始化API Scope
            if (!configurationDbContext.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    configurationDbContext.ApiScopes.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }
            ///ApiResource 提供给introspect 校验 
            if (!configurationDbContext.ApiResources.Any()) {
                foreach (var resource in Config.ApiResources)
                {
                    configurationDbContext.ApiResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }
               
        }

        public static void InitAspNetIdentityDb(IApplicationBuilder app)
        {
            // 创建服务范围
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            // 获取上下文对象
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                // 执行迁移
                context.Database.Migrate();
            }
            // 获取用户管理器
            var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            // 判断种子用户是否存在
            var admin = userMgr.FindByNameAsync("admin").Result;
            if (admin == null)
            {
                // 创建种子用户
                admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                    EmailConfirmed = true,
                };
                // 密码必须符合规则
                var result = userMgr.CreateAsync(admin, "123Qwe!").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                // 添加其它身份信息
                result = userMgr.AddClaimsAsync(admin, new[]{
                    new Claim(JwtClaimTypes.Name, "Administrator"),
                    new Claim(JwtClaimTypes.WebSite, "http://localhost"),
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        /// <summary>
        /// 自定义修改内容
        /// </summary>
        /// <param name="app"></param>
        public static void ChangeAspNetIdentityDb(IApplicationBuilder app)
        {
            // 创建服务范围
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            // 获取上下文对象
            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            // 执行迁移
            //context.Database.Migrate();
            var client = context.Clients.Where(x => x.ClientId == "test_pass_client").FirstOrDefault();
            if (client != null)
            {
                client.AllowOfflineAccess = true;//允许刷新token
                client.AccessTokenLifetime = 7200;//token 过期时间
                context.Clients.Update(client);
                context.SaveChanges();
            }
            var mvcClientEntity = context.Clients.Where(x => x.ClientId == "test_mvc_client").FirstOrDefault();
            var mvcClient = mvcClientEntity.ToModel();
            if (mvcClient != null)
            {
                mvcClient.AllowOfflineAccess = true;//允许刷新token
                mvcClient.AccessTokenLifetime = 7200;//token 过期时间
                context.Clients.Remove(mvcClientEntity);

                var tempClient = new IdentityServer4.Models.Client
                {
                    ClientId = "test_mvc_client",
                    ClientName = "test MVC Client",
                    ClientSecrets =
                {
                    new IdentityServer4.Models.Secret("test123".Sha256())
                },
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RedirectUris = { "https://localhost:4001/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:4001" },//退出登录后跳转地址
                    AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                    RequireConsent = true//是否授权 用户操作选择
                };
                context.Clients.Add(tempClient.ToEntity());
                context.SaveChanges();
            }
        }
    }
}