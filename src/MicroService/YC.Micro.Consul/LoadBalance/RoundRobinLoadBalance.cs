/*
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace YC.Micro.Consul
{
    /// <summary>
    /// 轮询算法
    /// </summary>
   public class RoundRobinLoadBalance : AbstractLoadBalance
    {
        private static int RECYCLE_PERIOD = 60000;

        public class WeightedRoundRobin
        {
            private static int weight;
            private static long current = 0;
            private static long lastUpdate;
            public static int GetWeight()
            {
                return weight;
            }
            public static void SetWeight(int weight)
            {
                WeightedRoundRobin.weight = weight;
                current = Interlocked.Add(ref current, 0);
            }
            public static long IncreaseCurrent()
            {
                return Interlocked.Add(ref current, weight);
            }
            public static void Sel(int total)
            {
                Interlocked.Add(ref current, -1 * total);
            }
            public static long GetLastUpdate()
            {
                return lastUpdate;
            }
            public static void setLastUpdate(long lastUpdate)
            {
                WeightedRoundRobin.lastUpdate = lastUpdate;
            }
        }

        private ConcurrentDictionary<string, ConcurrentDictionary<string, WeightedRoundRobin>> methodWeightMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, WeightedRoundRobin>>();
        private AtomicBoolean updateLock = new AtomicBoolean();


        /**
         * get invoker addr list cached for specified invocation
         * <p>
         * <b>for unit test only</b>
         * 
         * @param invokers
         * @param invocation
         * @return
        protected ICollection<string> getInvokerAddrList(IList<ServiceNode> serviceUrls)
        {
            string key = serviceUrls[0].Url;
            ConcurrentDictionary<string, WeightedRoundRobin> map = methodWeightMap[key];
            if (map != null)
            {
                return map.Keys;
            }
            return null;
        }
        public override ServiceNode DoSelect(IList<ServiceNode> serviceUrls)
        {
            string key = serviceUrls[0].Url;
            ConcurrentDictionary<string, WeightedRoundRobin> map = methodWeightMap[key];
            if (map == null)
            {
                methodWeightMap.TryAdd(key, new ConcurrentDictionary<string, WeightedRoundRobin>());
                map = methodWeightMap[key];
            }
            int totalWeight = 0;
            long maxCurrent = long.MaxValue;
            long now = DateTime.Now.ToFileTimeUtc();
            ServiceNode serviceUrl = null;
            WeightedRoundRobin selectedWRR = null;
            foreach (ServiceNode url in serviceUrls)
            {
                string identifyString = url.Url;
                WeightedRoundRobin weightedRoundRobin = map[identifyString];
                int weight = GetWeight();
                if (weight < 0)
                {
                    weight = 0;
                }
                if (weightedRoundRobin == null)
                {
                    weightedRoundRobin = new WeightedRoundRobin();
                    weightedRoundRobin.SetWeight(weight);
                    map.TryAdd(identifyString, weightedRoundRobin);
                    weightedRoundRobin = map[identifyString];
                }
                if (weight != weightedRoundRobin.GetWeight())
                {
                    //weight changed
                    weightedRoundRobin.SetWeight(weight);
                }
                long cur = weightedRoundRobin.IncreaseCurrent();
                weightedRoundRobin.SetLastUpdate(now);
                if (cur > maxCurrent)
                {
                    maxCurrent = cur;
                    serviceUrl = url;
                    selectedWRR = weightedRoundRobin;
                }
                totalWeight += weight;
            }
            if (!updateLock.get() && serviceUrls.Count != map.Count)
            {
                if (updateLock.compareAndSet(false, true))
                {
                    try
                    {
                        // copy -> modify -> update reference
                        ConcurrentDictionary<String, WeightedRoundRobin> newMap = new ConcurrentDictionary<String, WeightedRoundRobin>();
                       // newMap.TryUpdate(map);
                        IEnumerator<string> it = newMap.Keys.GetEnumerator();
                        while (it.MoveNext())
                        {
                            
                            if (now - newMap[it.Current].GetLastUpdate() > RECYCLE_PERIOD)
                            {
                                it.Dispose();
                            }
                        }
                        methodWeightMap.TryAdd(key, newMap);
                    }
                    finally
                    {
                        updateLock.set(false);
                    }
                }
            }
            if (serviceUrl != null)
            {
                selectedWRR.Sel(totalWeight);
                return serviceUrl;
            }
            // should not happen here
            return serviceUrls[0];
        }
    }
}
*/