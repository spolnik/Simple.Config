using System;
using System.Threading;
using NUnit.Framework;

namespace Simple.Config.Tests.StressTests
{
    public class ConcurencyTests_functional_tests
    {
        private const int NumberOfThreads = 10;

        private ConfigManager _configManager;

        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [Test]
        public void TestMany_NamespacesXml()
        {
            LoadBenchmark("stressManyNS.xml");
        }

        [Test]
        public void TestMany_PropertiesXml()
        {
            LoadBenchmark("stressManyPP.xml");
        }

        [Test]
        public void TestMany_ValuesXml()
        {
            LoadBenchmark("stressManyVL.xml");
        }

        [Test]
        public void Test_BigXml()
        {
            LoadBenchmark("stressBig.xml");
        }

        [Test]
        public void Test_NormalXml()
        {
            LoadBenchmark("stressNormal.xml");
            Console.Out.WriteLine();
        }

        [Test]
        public void TestMany_NamespacesIni()
        {
            LoadBenchmark("stressManyNS.ini");
        }

        [Test]
        public void TestMany_PropertiesIni()
        {
            LoadBenchmark("stressManyPP.ini");
        }

        [Test]
        public void TestMany_ValuesIni()
        {
            LoadBenchmark("stressManyVL.ini");
        }

        [Test]
        public void Test_BigIni()
        {
            LoadBenchmark("stressBig.ini");
        }

        [Test]
        public void Test_NormalIni()
        {
            LoadBenchmark("stressNormal.ini");
            Console.Out.WriteLine();
        }

        private string _filename;
        private readonly int[] _config = new int[3];

        
        public void LoadBenchmark(string filename)
        {
            long start = Environment.TickCount;

            _filename = filename;
            var threads = new Thread[NumberOfThreads];

            for (var i = 0; i < NumberOfThreads; i++)
            {
                threads[i] = new Thread(Run);
                threads[i].Start();
            }

            for (var i = 0; i < NumberOfThreads; i++)
                threads[i].Join();

            long stop = Environment.TickCount;

            Console.Out.WriteLine("{0}ms / [{1} namespaces x {2} properties x {3} values = {4}] {5} ({6} threads)",
                                  (stop - start)/NumberOfThreads, _config[0], _config[1], _config[2],
                                  (_config[0]*_config[1]*_config[2]), filename, NumberOfThreads);
        }

        private void Run()
        {
            lock (_configManager)
            {
                _configManager.Clear();
                _configManager.Load(@"test_files\stress\" + _filename);
            }

            _config[0] = _configManager.Namespaces.Length;
            _config[1] = _configManager.Namespaces[0].Properties.Length;
            _config[2] = _configManager.Namespaces[0].Properties[0].Values.Count;
        }
    }
}
