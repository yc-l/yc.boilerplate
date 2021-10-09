using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Common.ShareUtils
{
  public  class RandomUtils
    {
        /// <summary>
        /// 描 述:创建加密随机数生成器 生成强随机种子
        /// </summary>
        /// <returns></returns>
      private  static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 获取真正随机数
        /// </summary>
        /// <param name="numMin"></param>
        /// <param name="numMax"></param>
        /// <returns></returns>
        public static int GetRandom(int numMin=0,int numMax=10000) {

            int ranNumer = new Random(GetRandomSeed()).Next(numMin, numMax);

            return ranNumer;
        }
    }
}
