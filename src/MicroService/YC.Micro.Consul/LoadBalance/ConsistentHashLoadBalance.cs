/*
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace YC.Micro.Consul
{
    /// <summary>
    /// Hash一致性算法
    /// </summary>
    public class ConsistentHashLoadBalance : AbstractLoadBalance
    {
        private ConcurrentDictionary<string, ConsistentHashSelector> selectors = new ConcurrentDictionary<string, ConsistentHashSelector>();

        public override ServiceNode DoSelect(IList<ServiceNode> serviceUrls)
        {
            string key = serviceUrls[0].Url;
            int identityHashCode = serviceUrls.GetHashCode();
            ConsistentHashSelector selector = (ConsistentHashSelector)selectors[key];
            if (selector == null || selector.GetHashCode() != identityHashCode)
            {
                selectors.TryAdd(key, new ConsistentHashSelector(serviceUrls, "", identityHashCode));
                selector = (ConsistentHashSelector)selectors[key];
            }
            return selector.Select(key);
        }

        private class ConsistentHashSelector
        {

            private SortedDictionary<long, ServiceNode> virtualServiceUrls;

            private int replicaNumber;

            private int identityHashCode;

            private int[] argumentIndex;
            private IList<ServiceNode> serviceUrls;
            private string v;

            public ConsistentHashSelector(IList<ServiceNode> serviceUrls, string methodName, int identityHashCode)
            {
                this.virtualServiceUrls = new SortedDictionary<long, ServiceNode>();
                this.identityHashCode = identityHashCode;
                string url = serviceUrls[0].Url;
                this.replicaNumber = 160;// 默认多少个虚拟节点
                string[] index = new string[] { };
                argumentIndex = new int[index.Length];
                for (int i = 0; i < index.Length; i++)
                {
                    argumentIndex[i] = int.Parse(index[i]);
                }
                foreach (ServiceNode serviceUrl in serviceUrls)
                {
                    string address = serviceUrl.Url;
                    for (int i = 0; i < replicaNumber / 4; i++)
                    {
                        byte[] digest = md5(address + i);
                        for (int h = 0; h < 4; h++)
                        {
                            long m = hash(digest, h);
                            virtualServiceUrls.Add(m, serviceUrl);
                        }
                    }
                }
            }

            public ServiceNode Select(string url)
            {
                string key = url;
                byte[] digest = md5(key);
                return selectForKey(hash(digest, 0));
            }

            private string toKey(Object[] args)
            {
                StringBuilder buf = new StringBuilder();
                foreach (int i in argumentIndex)
                {
                    if (i >= 0 && i < args.Length)
                    {
                        buf.Append(args[i]);
                    }
                }
                return buf.ToString();
            }

            private ServiceNode selectForKey(long hash)
            {
                KeyValuePair<long, ServiceNode> entry = virtualServiceUrls.GetEnumerator().Current;
                if ("null".Equals(entry) || "".Equals(entry))
                {
                    entry = virtualServiceUrls.GetEnumerator().Current;
                }
                return entry.Value;
            }

            private long hash(byte[] digest, int number)
            {
                return (((long)(digest[3 + (number * 4)] & 0xFF) << 24)
                        | ((long)(digest[2 + number * 4] & 0xFF) << 16)
                        | ((long)(digest[1 + number * 4] & 0xFF) << 8)
                        | (digest[number * 4] & 0xFF))
                        & 0xFFFFFFFFL;
            }

            private byte[] md5(string value)
            {
                var hashed = EncryptProvider.Md5(value);
                byte[] bytes = Encoding.UTF8.GetBytes(hashed);
                return bytes;
            }

        }
    }
}
*/