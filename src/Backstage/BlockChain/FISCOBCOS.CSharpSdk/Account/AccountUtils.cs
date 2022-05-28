using FISCOBCOS.CSharpSdk.Dto;
using FISCOBCOS.CSharpSdk.Utis;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk
{
    public class AccountUtils
    {
        /// <summary>
        /// 自动生成一个用户(公钥，私钥，地址)
        /// </summary>
        /// <param name="userName">自定义用户名称</param>
        /// <returns></returns>
        public static AccountDto GeneratorAccount(string userName)
        {

            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var publicKey = "0x" + ecKey.GetPubKeyNoPrefix().ToHex();//规则是要去除前缀后的16进制，
            var privateKey = ecKey.GetPrivateKey().RemoveHexPrefix();
            var accountAddress = new Account(privateKey).Address.ToLower();//address 

            AccountDto accountDto = new AccountDto();
            accountDto.Address = accountAddress;
            accountDto.PrivateKey = privateKey;
            accountDto.PublicKey = publicKey;
            accountDto.UserName = userName;
            accountDto.Type = 0;
            return accountDto;

        }
        /// <summary>
        /// 通过私钥生成公钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>返回公钥</returns>
        public static string GeneratorPublicKeyByPrivateKey(string privateKey)
        {
            var ecKey = new Nethereum.Signer.EthECKey(privateKey);
            var publicKey = "0x" + ecKey.GetPubKeyNoPrefix().ToHex();
            return publicKey;
        }

        /// <summary>
        ///通过私钥获得帐户地址
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string GetAddressByPrivateKey(string privateKey)
        {
            var accountAddress = new Account(privateKey).Address.ToLower();//address 
            return accountAddress;
        }
    }
}
