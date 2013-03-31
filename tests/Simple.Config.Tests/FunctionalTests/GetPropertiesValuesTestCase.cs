using System;
using NUnit.Framework;

namespace Simple.Config.Tests.FunctionalTests
{
    public class GetPropertiesValuesTestCase
    {
        private ConfigManager _configManager;
        
        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [Test]
        public void Test_GetValue()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");
            
            String value = _configManager.GetValue("section1", "prop1");
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Equals("value1"));
            
            value = _configManager.GetValue("section1", "prop2");
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Equals("value1"));
            
            value = _configManager.GetValue("section1", "prop3");
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Equals("value1"));
            
            value = _configManager.GetValue("section2", "prop1");
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Equals("value1"));
            
            value = _configManager.GetValue("section2", "prop2");
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Equals("value1"));
            
            value = _configManager.GetValue("section2", "prop3");
            Assert.IsNotNull(value);
            Assert.IsTrue(value.Equals("value1"));
            
            _configManager.RemoveFile(@"test_files\test.ini");
        }
        
        [Test]
        public void Test_GetValues()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");
            
            var values = _configManager.GetValues("section1", "prop1");
            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 1);
            Assert.IsTrue(values[0].Equals("value1"));
            
            values = _configManager.GetValues("section1", "prop2");
            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 2);
            Assert.IsTrue(values[0].Equals("value1") && 
                             values[1].Equals("value2"));
            
            values = _configManager.GetValues("section1", "prop3");
            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 3);
            Assert.IsTrue(values[0].Equals("value1") && 
                             values[1].Equals("value2") && 
                             values[2].Equals("value3"));
            
            values = _configManager.GetValues("section2", "prop1");
            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 1);
            Assert.IsTrue(values[0].Equals("value1"));
            
            values = _configManager.GetValues("section2", "prop2");
            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 2);
            Assert.IsTrue(values[0].Equals("value1") && 
                             values[1].Equals("value2"));
            
            values = _configManager.GetValues("section2", "prop3");
            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 3);
            Assert.IsTrue(values[0].Equals("value1") && 
                             values[1].Equals("value2") && 
                             values[2].Equals("value3"));
            
            _configManager.RemoveFile(@"test_files\test.ini");
        }
    }
}
