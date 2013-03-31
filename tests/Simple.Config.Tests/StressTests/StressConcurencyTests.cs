using System;
using NUnit.Framework;

namespace Simple.Config.Tests.StressTests
{
    public class StressConcurencyTests
    {
        private int _numberOfTimesToRunTest;

        private StressTestsUtils _stressTestsUtils;

        private ConfigManager _configManager;

        private Exception _exception;

        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
            _stressTestsUtils = new StressTestsUtils {Units = " config per sec"};
        }

        private void RunGetValue()
        {
            try
            {
                for (var i = 0; i < _numberOfTimesToRunTest; i++)
                    _configManager.GetValue("nsValue", "aaa");

                var val = _configManager.GetValue("nsValue", "aaa");
                Assert.AreEqual("ccccc", val, "string value");
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        [Test]
        public void TestGetValue()
        {
            _configManager.Clear();
            _configManager.Load("test_files\\stress\\stressValue.xml");

            _stressTestsUtils.RunInThreads("ConfigManager", "Get value", 100, 10, RunGetValue, out _numberOfTimesToRunTest);
            
            if (_exception != null)
                throw _exception;
        }

    }
}
