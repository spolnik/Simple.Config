using System;
using NUnit.Framework;

namespace Simple.Config.Tests.StressTests
{
    public class BenchmarkTests_functional_tests
    {
        // Aproximate time to run one test.
        private const int Time = 3000;
    	
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

        private void LoadBenchmark(string filename) 
        {        
            long start = Environment.TickCount;
            long count = 0;
            var config = new int[3];

            while (Environment.TickCount - start < Time) 
            {
                _configManager.Clear();
                _configManager.Load(@"test_files\stress\" + filename);
                
                config[0] = _configManager.Namespaces.Length;
                config[1] = _configManager.Namespaces[0].Properties.Length;
                config[2] = _configManager.Namespaces[0].Properties[0].Values.Count;
                count++;
            }

            long stop = Environment.TickCount;

            Console.Out.WriteLine("{0}ms / [{1} namespaces x {2} properties x {3} values = {4}] {5} ({6} runs)",
                                  (stop - start)/count, config[0], config[1], config[2], (config[0]*config[1]*config[2]),
                                  filename, count);
        }                
    }
}
