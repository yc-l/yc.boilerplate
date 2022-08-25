using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YC.ApplicationService.DefaultConfigure;
using YC.Common;
using YC.Common.ShareUtils;
using System.Linq;
using YC.ApplicationService.DefaultConfigure.Dto;
using FreeSql;
using YC.FreeSqlFrameWork;
using YC.Model.SysDbEntity;
using AutoMapper;
using CSRedis;
using YC.Core.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace YC.ApplicationService
{
    /// <summary>
    /// 默认配置
    /// </summary>
    public class DefaultConfig
    {
        /// <summary>
        /// 如果是应用，默认要在开启时候，做一次设置动作
        /// </summary>
        public static string dbConfigFilePath = System.Environment.CurrentDirectory + "//DefaultConfig.json";

        /// <summary>
        /// 租户session 前缀
        /// </summary>
        public const string SESSION_TENANT = "TENANT_";

        /// <summary>
        ///用户session 前缀
        /// </summary>
        public const string SESSIONT_TENANT_USER = "TENANT_{0}_USER";

        /// <summary>
        /// //登录验证码
        /// </summary>
        public const string SESSION_VERIFICATIONCODE = "LOGIN_VERIFICATIONCODE";

        /// <summary>
        /// 用户缓存前缀
        /// </summary>
        public const string CACHE_TOKEN_USER = "CACHE_TOKEN_USER_{0}";

        /// <summary>
        /// 用户刷新缓存前缀
        /// </summary>
        public const string CACHE_RETOKEN_USER = "CACHE_RETOKEN_USER_{0}";

        /// <summary>
        /// 用户角色权限
        /// </summary>
        public const string CACHE_USER_ROLE_PEMISSION = "CACHE_USER_ROLE_PEMISSION_{0}";

        /// <summary>
        /// 全局Tenant配置
        /// </summary>
        public const string CACHE_TENANT_CONFIG = "CACHE_TENANT_CONFIG";

        /// <summary>
        /// 数据库配置
        /// </summary>
        public static DataBaseConfig DbConfig
        {
            get
            {
                return JsonConfig.ToObject<DataBaseConfig>();
            }
        }

        /// <summary>
        /// 租户配置
        /// </summary>
        public static TenantSetting TenantSetting
        {
            get
            {
                //var tenantData = JsonConfig.GetOjectByJsonKey<TenantSetting>("TenantSetting");
                //return tenantData;
                using (var cache = new CacheUtils())
                {
                    var tenantData = JsonConfig.GetOjectByJsonKey<TenantSetting>("TenantSetting");
                    var cacheTenantInfo = cache.Get<TenantSetting>(CACHE_TENANT_CONFIG);
                    // var cacheTenantInfo = RedisHelper.Get(CACHE_TENANT_CONFIG);
                    if (cacheTenantInfo != null)
                    {
                        tenantData = cacheTenantInfo;
                        return tenantData;
                    }
                    else
                    {
                        //数据变成从数据库中读取
                        using (var freeSql = FreeSqlUtils.GetFreeSql(tenantData.DefaultDbType, tenantData.DefaultDbConnectionString))
                        {
                            //默认只获取启用
                            var list = freeSql.GetRepository<SysTenant>().Select.Where(x => x.IsActive == true).ToList();
                            var tempList = new List<TenantInfo>();

                            MapperConfiguration config = new MapperConfiguration(fig => fig.AddProfile<MapConfig>());
                            IMapper mapper = new Mapper(config);
                            //进行数据组合，将原来没有租户信息加到数据库
                            for (int i = 0; i < list.Count; i++)
                            {
                                var tempObj = mapper.Map<TenantInfo>(list[i]);
                                bool isExist = tenantData.TenantList.Any(x => x.TenantId == tempObj.TenantId);
                                if (isExist)//存在就删除旧数据，之后更新最新数据
                                {
                                    int removeCount = tenantData.TenantList.RemoveAll(x => x.TenantId == tempObj.TenantId);
                                }
                                tenantData.TenantList.Add(tempObj);
                            }
                        }
                        cache.Set_AbsoluteExpire(CACHE_TENANT_CONFIG, tenantData, TimeSpan.FromSeconds(600));
                        //RedisHelper.Set(CACHE_TENANT_CONFIG, System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(tenantData), TimeSpan.FromSeconds(600));
                        return tenantData;
                    }
                }
            }
        }

        /// <summary>
        /// 默认app设置
        /// </summary>
        public static DefaultAppConfig DefaultAppConfig
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<DefaultAppConfig>("AppSetting");
            }
        }

        public static CSRedisClient GetRedisClient()
        {
            CSRedisClient csredis = new CSRedis.CSRedisClient(DefaultAppConfig.RedisConnectionString);
            return csredis;
        }

        public static void InitializationRedis()
        {
            RedisHelper.Initialization(GetRedisClient());// 直接用
        }

        /// <summary>
        /// es配置
        /// </summary>
        public static ElasticSearchSeting ElasticSearchSeting
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<ElasticSearchSeting>("ElasticSearchSeting");
            }
        }

        /// <summary>
        /// 允许不需要token访问URL
        /// </summary>
        public static List<AllowedNoTokenUrl> AllowedNoTokenUrls
        {
            get
            {
                return DefaultConfig.DefaultAppConfig.AllowedNoTokenUrls;
            }
        }

        /// <summary>
        /// 允许不做权限校验url
        /// </summary>
        public static string[] AllowedNoPermissionUrls
        {
            get
            {
                return DefaultConfig.DefaultAppConfig.AllowedNoPermissionUrls.Select(x => x.Url).ToArray();
            }
        }

        /// <summary>
        /// 允许tokens
        /// </summary>
        public static string[] AllowedTokens
        {
            get
            {
                return DefaultConfig.DefaultAppConfig.AllowedTokens.Select(x => x.Token).ToArray();
            }
        }

        /// <summary>
        /// es 集群节点
        /// </summary>
        public static string[] ElasticSearchNodes
        {
            get
            {
                return DefaultConfig.ElasticSearchSeting.Nodes.Select(x => x.Node).ToArray();
            }
        }

        /// <summary>
        /// json配置
        /// </summary>
        public static string JsonConfig { get; set; }

        /// <summary>
        /// redis 配置
        /// </summary>
        public static ConnectionRedis ConnectionRedis => DbConfig.ConnectionRedis;

        /// <summary>
        /// 获取指定的json对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T GetJsonList<T>(string json) where T : class
        {
            T tempJsonData = json.ToObject<T>();
            return tempJsonData;
        }

        /// <summary>
        /// 获取指定的json字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetConfigJson(string path)
        {
            string jsonfile = path;

            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var o = JToken.ReadFrom(reader);
                    return o.ToString();
                }
            }
        }

        /// <summary>
        /// 修改json配置
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SetConfigJson(string path, string content)
        {
            string error = "";
            bool result = FileUtils.CoverWriteFile(path, content, out error);
            return result;
        }
    }
}