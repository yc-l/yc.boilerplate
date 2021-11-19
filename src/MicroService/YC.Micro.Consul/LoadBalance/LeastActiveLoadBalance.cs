/*
using System;
using System.Collections.Generic;
using System.Text;

namespace YC.Micro.Consul
{
    /// <summary>
    /// 最少活跃数算法
    /// 核心在于记住每个url的状态
    /// </summary>
    class LeastActiveLoadBalance : AbstractLoadBalance
    {
        private readonly Random random = new Random();

        public override ServiceUrl DoSelect(IList<ServiceUrl> serviceUrls)
        {
            int length = serviceUrls.Count; // Number of invokers
            int leastActive = -1; // The least active value of all invokers
            int leastCount = 0; // The number of invokers having the same least active value (leastActive)
            int[] leastIndexs = new int[length]; // The index of invokers having the same least active value (leastActive)
            int totalWeight = 0; // The sum of with warmup weights
            int firstWeight = 0; // Initial value, used for comparision
            bool sameWeight = true; // Every invoker has the same weight value?
            for (int i = 0; i < length; i++)
            {
                ServiceUrl serviceUrl = serviceUrls[i];
                int active = 10; // Active number(活跃数，是状态模式)
                int afterWarmup = GetWeight(); // Weight
                if (leastActive == -1 || active < leastActive)
                { // Restart, when find a invoker having smaller least active value.
                    leastActive = active; // Record the current least active value
                    leastCount = 1; // Reset leastCount, count again based on current leastCount
                    leastIndexs[0] = i; // Reset
                    totalWeight = afterWarmup; // Reset
                    firstWeight = afterWarmup; // Record the weight the first invoker
                    sameWeight = true; // Reset, every invoker has the same weight value?
                }
                else if (active == leastActive)
                { // If current invoker's active value equals with leaseActive, then accumulating.
                    leastIndexs[leastCount++] = i; // Record index number of this invoker
                    totalWeight += afterWarmup; // Add this invoker's weight to totalWeight.
                                                // If every invoker has the same weight?
                    if (sameWeight && i > 0
                            && afterWarmup != firstWeight)
                    {
                        sameWeight = false;
                    }
                }
            }
            // assert(leastCount > 0)
            if (leastCount == 1)
            {
                // If we got exactly one invoker having the least active value, return this invoker directly.
                return serviceUrls[leastIndexs[0]];
            }
            if (!sameWeight && totalWeight > 0)
            {
                // If (not every invoker has the same weight & at least one invoker's weight>0), select randomly based on totalWeight.
                int offsetWeight = random.Next(totalWeight) + 1;
                // Return a invoker based on the random value.
                for (int i = 0; i < leastCount; i++)
                {
                    int leastIndex = leastIndexs[i];
                    offsetWeight -= GetWeight();
                    if (offsetWeight <= 0)
                        return serviceUrls[leastIndex];
                }
            }
            // If all invokers have the same weight value or totalWeight=0, return evenly.
            return serviceUrls[leastIndexs[random.Next(leastCount)]];
        }
    }
}
*/