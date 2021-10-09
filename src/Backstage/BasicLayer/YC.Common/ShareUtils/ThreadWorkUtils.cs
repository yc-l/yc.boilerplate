using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace YC.Common.ShareUtils
{
    public class ThreadWorkUtils
    {
        public Thread[] threads;
        public long initTimes = 0;
        public Dictionary<int, ThreadWorkInfo> dic = new Dictionary<int, ThreadWorkInfo>();

        public Tuple<long> Init(int threadCount, ParameterizedThreadStart workJob, ThreadWorkInfo threadWorkInfo)
        {

            threads = new Thread[threadCount];//创建指定数量的线程
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(workJob);
                dic[threads[i].ManagedThreadId] = threadWorkInfo;
            }
            long initTimes = sw.ElapsedMilliseconds;
            sw.Stop();
            return Tuple.Create(initTimes);

        }

        public Tuple<long> StartAll()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long workTimes = 0;
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
                threads[i].IsBackground = true;
              
            }
            long initTimes = sw.ElapsedMilliseconds;
            sw.Stop();
            return Tuple.Create(workTimes);

        }


        public void RecoveryAll()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long workTimes = 0;
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Resume();
              

            }
           

        }


        public Tuple<long> AbortAll()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long workTimes = 0;
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Abort();
            }
            long initTimes = sw.ElapsedMilliseconds;
            sw.Stop();
            return Tuple.Create(workTimes);
        }

        public  void ThreadPoolWork(WaitCallback callback, int minThreadCount = 1, int maxThreadCount = 10)
        {
            ThreadPool.SetMaxThreads(minThreadCount, maxThreadCount);
            ThreadPool.QueueUserWorkItem(callback);
        }
    }
}

public class ThreadWorkInfo
{

    public bool State { get; set; }
    public int Milliseconds { get; set; }

}
