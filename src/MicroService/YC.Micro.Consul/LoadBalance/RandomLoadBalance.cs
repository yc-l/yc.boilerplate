
using System;
using System.Collections.Generic;

namespace YC.Micro.Consul
{
    /// <summary>
    /// 加权随机算法
    /// </summary>
    public class RandomLoadBalance : AbstractLoadBalance
    {
        private readonly Random random = new Random();

        public override ConsulServiceNode DoSelect(IList<ConsulServiceNode> serviceUrls)
        {
            int length = serviceUrls.Count; // Number of serviceUrls
            int totalWeight = 0; // The sum of weights
            bool sameWeight = true; // Every serviceUrls has the same weight?
            for (int i = 0; i < length; i++)
            {
                int weight = GetWeight();
                totalWeight += weight; // Sum
                if (sameWeight && i > 0
                        && weight != GetWeight())
                {
                    sameWeight = false;
                }
            }
            if (totalWeight > 0 && !sameWeight)
            {
                // If (not every invoker has the same weight & at least one invoker's weight>0), select randomly based on totalWeight.
                int offset = random.Next(totalWeight);
                // Return a invoker based on the random value.
                for (int i = 0; i < length; i++)
                {
                    offset -= GetWeight();
                    if (offset < 0)
                    {
                        return serviceUrls[i];
                    }
                }
            }
            // If all invokers have the same weight value or totalWeight=0, return evenly.
            return serviceUrls[random.Next(length)];
        }
        public override string DoSelectOne(IList<string> serviceUrls)
        {
            int length = serviceUrls.Count; // Number of serviceUrls
            int totalWeight = 0; // The sum of weights
            bool sameWeight = true; // Every serviceUrls has the same weight?
            for (int i = 0; i < length; i++)
            {
                int weight = GetWeight();
                totalWeight += weight; // Sum
                if (sameWeight && i > 0
                        && weight != GetWeight())
                {
                    sameWeight = false;
                }
            }
            if (totalWeight > 0 && !sameWeight)
            {
                // If (not every invoker has the same weight & at least one invoker's weight>0), select randomly based on totalWeight.
                int offset = random.Next(totalWeight);
                // Return a invoker based on the random value.
                for (int i = 0; i < length; i++)
                {
                    offset -= GetWeight();
                    if (offset < 0)
                    {
                        return serviceUrls[i];
                    }
                }
            }
            // If all invokers have the same weight value or totalWeight=0, return evenly.
            return serviceUrls[random.Next(length)];
        }
    }
}