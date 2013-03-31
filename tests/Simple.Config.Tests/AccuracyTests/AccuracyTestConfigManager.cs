using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Simple.Config.Tests.AccuracyTests
{
    public class AccuracyTestConfigManger
    {
        private const string XmlConfigFile = "test_files/accuracy.xml";

        private ConfigManager _configManager;

        [SetUp]
        public void SetUp() 
        {
            _configManager = ConfigManager.GetInstance();

            if (_configManager.GetConfigFile(XmlConfigFile) == null)
                _configManager.Load(XmlConfigFile);
        }

        [TearDown]
        public void TearDown()
        {
            _configManager.RemoveFile(XmlConfigFile);
        }
        
        [Test]
        public void Test_GetValue()
        {
            Assert.AreEqual("12345", _configManager.GetValue("AccuracyNs3", "int_value"));
            Assert.AreEqual("0.1", _configManager.GetValue("AccuracyNs3", "double_value"));
            Assert.AreEqual("a", _configManager.GetValue("AccuracyNs3", "string_value"));
        }

        [Test]
        public void Test_GetValues()
        {
            var intValues = _configManager.GetValues("AccuracyNs3", "int_values").Select(x => Convert.ToInt32(x)).ToArray();
            Array.Sort(intValues);
            Assert.AreEqual(-1, intValues[0]);
            Assert.AreEqual(2, intValues[1]);
            Assert.AreEqual(3, intValues[2]);

            var doubleValues = _configManager.GetValues("AccuracyNs3", "double_values").Select(x => Convert.ToDouble(x, CultureInfo.InvariantCulture)).ToArray();
            Array.Sort(doubleValues);
            Assert.AreEqual(-0.1, doubleValues[0]);
            Assert.AreEqual(0.1, doubleValues[1]);
            Assert.AreEqual(1e30, doubleValues[2]);

            var stringValues = _configManager.GetValues("AccuracyNs3", "string_values").ToArray();
            Array.Sort(stringValues);
            Assert.AreEqual(3, stringValues.Length);
            Assert.AreEqual("a", stringValues[0]);
            Assert.AreEqual("b", stringValues[1]);
            Assert.AreEqual("c", stringValues[2]);
        }
    }
}
