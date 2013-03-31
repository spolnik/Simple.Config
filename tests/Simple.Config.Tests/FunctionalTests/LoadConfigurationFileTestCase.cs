using NUnit.Framework;

namespace Simple.Config.Tests.FunctionalTests
{
    public class LoadConfigurationFileTestCase
    {
        private ConfigManager _configManager;
        
        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [Test]
        public void Test_Instantiation()
        {
            Assert.IsNotNull(_configManager);

            var namespaceObject = _configManager.Namespaces;
            Assert.IsNotNull(namespaceObject);
            Assert.IsTrue(namespaceObject.Length == 2);
            Assert.IsNotNull(_configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests1"));
            Assert.IsNotNull(_configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests2"));
        }
        
        [Test]
        public void Test_LoadingXml()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test3.xml");

            var namespaceObject = cf.Namespaces;
            Assert.IsNotNull(namespaceObject);
            Assert.IsTrue(namespaceObject.Count == 1);

            Assert.IsNotNull(_configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests3"));
            var values = _configManager.GetValues("Simple.Config.ConfigurationManager.FunctionalTests3", "p1");

            Assert.IsNotNull(values);
            Assert.IsTrue(values.Count == 2);
            Assert.IsTrue(values[0].Equals("v1") && values[1].Equals("v2"));

            _configManager.RemoveFile(@"test_files\test3.xml");
        }
        
        [Test]
        public void Test_LoadingIni()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            var namespaceObject = cf.Namespaces;
            Assert.IsNotNull(namespaceObject);
            Assert.IsTrue(namespaceObject.Count == 2);
            Assert.IsNotNull(_configManager.GetNamespace("section1"));
            Assert.IsNotNull(_configManager.GetNamespace("section2"));
            
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
