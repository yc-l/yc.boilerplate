using IdentityModel.Client;
using IdentityModel.Jwk;
using IdentityServer4.Validation;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTestClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest()
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "test_pass_client",
                    ClientSecret = "test123",
                    UserName = "admin",
                    Password = "123Qwe!"
                });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            var jwks = await client.GetAsync("https://localhost:5001/.well-known/openid-configuration/jwks");
            if (jwks.IsSuccessStatusCode)
            {
                var jwksContent = await jwks.Content.ReadAsStringAsync();

                var rsaKey = JObject.Parse(jwksContent).GetValue("keys")[0];
                var jwk = new Microsoft.IdentityModel.Tokens.JsonWebKey(rsaKey.ToString());

                SigningCredentials credentials = new SigningCredentials(jwk, jwk.Alg);

                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
                //jwtHandler.InboundClaimTypeMap.Clear();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKeys = new List<SecurityKey>() { jwk },
                    ValidateIssuerSigningKey = true,//验证issuer 提供者的签名key

                    ValidIssuer = "https://localhost:5001",//提供者
                    ValidateIssuer = true,

                    //ValidAudience = "",
                    ValidateAudience = false,

                    RequireSignedTokens = true,
                    RequireExpirationTime = true
                };

                var jwtArr = tokenResponse.AccessToken.Split('.');

                var result = jwtHandler.ValidateToken(tokenResponse.AccessToken, tokenValidationParameters, out SecurityToken token);

                
            }
            //Console.ReadLine();

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            string bookRequest = "https://localhost:5000/AggregateService/Api/Index/GetBookList?queryFilterString=吞噬星空&currentPage=1&pageSize=100";

            string userRequest = "https://localhost:5000/AggregateService/Api/Index/GetUserList?id=1";
             var response = await apiClient.GetAsync(userRequest);
                                       

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JObject.Parse(content));

            Console.ReadLine();
        }
    }
}