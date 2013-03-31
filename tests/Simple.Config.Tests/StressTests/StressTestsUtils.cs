using System;
using System.Threading;

namespace Simple.Config.Tests.StressTests
{
    public class StressTestsUtils
    {
        public string Units = "units";

        internal void RunInThreads(string testedClass, string name, int account, int nThreads, ThreadStart threadStart, out int pcnt)
        {
            pcnt = account / nThreads;
            var tt = new Thread[nThreads];

            for (var i = 0; i < nThreads; i++)
                tt[i] = new Thread(threadStart);

            var t0 = DateTime.Now;
            for (var i = 0; i < nThreads; i++)
                tt[i].Start();

            for (var i = 0; i < nThreads; i++)
                tt[i].Join();

            var t1 = DateTime.Now;
            var ts = t1 - t0;
            var millis = (long)ts.TotalMilliseconds;
            var wps = account / ts.TotalSeconds;

            Console.WriteLine("{0} {1}, {2} threads {3} words took {4} millis {5:f2} " + Units,
                              testedClass, name, nThreads, account, millis, wps);
        }

        private int _seed = 999333;

        /// <summary>
        ///     random seed generator which should somewhat work with many threads.
        ///     (better than timer which can probably provide equal seeds)
        /// </summary>
        /// 
        /// <returns>new seed</returns>
        internal int NextSeed()
        {
            _seed = 123 + _seed * 98747;
            return _seed;
        }
    }
}
