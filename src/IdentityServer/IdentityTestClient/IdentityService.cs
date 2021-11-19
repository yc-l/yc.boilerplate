using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTestClient
{
    public class IdentityService
    {
        private HttpClient client;
        private DiscoveryDocumentResponse disco;
        private TokenResponse tokenResponse;
        /// <summary>
        /// 1、请求IdentityServer 连接
        /// </summary>
        /// <returns></returns>
        public async Task SetIdenttityRequest()
        {

            client = new HttpClient();

            //var discoveryClient = new DiscoveryClient(ids4Url) { Policy = { RequireHttps = false } };

            disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                string exceptionStr= "第一步异常：" +disco.Error;
                throw new Exception(exceptionStr);
            }

        }

        /// <summary>
        /// 2、请求获取Token
        /// </summary>
        /// <returns></returns>
        public async Task RquestToken()
        {
            // request token
             tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                GrantType= "client_credentials",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
        }

        /// <summary>
        /// 3、请求api
        /// </summary>
        /// <returns></returns>
        public async Task RequestApi()
        {

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://localhost:6001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("第三步异常："+response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

        }
    }
}
