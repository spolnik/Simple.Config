using NUnit.Framework;

namespace Simple.Config.Tests.FunctionalTests
{
    public class NavigateNamespacesAndPropertiesTestCase
    {
        private ConfigManager _configManager;
        
        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [Test]
        public void TestConfigFile_Namespaces()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            var namespaceObject = cf.Namespaces;

            Assert.IsNotNull(namespaceObject);
            Assert.IsTrue(namespaceObject.Count == 2);
            Assert.IsNotNull(namespaceObject[0]);
            Assert.IsNotNull(namespaceObject[1]);
            Assert.IsTrue(namespaceObject[0].Name.Equals("section1"));
            Assert.IsTrue(namespaceObject[1].Name.Equals("section2"));

            _configManager.RemoveFile(@"test_files\test.ini");
        }
        
        [Test]
        public void TestConfigManager_Namespaces()
        {
            Assert.IsNotNull(_configManager);
            var namespaceObject = _configManager.Namespaces;

            Assert.IsNotNull(namespaceObject);
            Assert.IsTrue(namespaceObject.Length == 2);
            Assert.IsNotNull(namespaceObject[0]);
            Assert.IsNotNull(namespaceObject[1]);

            Assert.IsTrue(namespaceObject[0].Name.Equals("Simple.Config.ConfigurationManager.FunctionalTests1"));
            Assert.IsTrue(namespaceObject[1].Name.Equals("Simple.Config.ConfigurationManager.FunctionalTests2"));
        }
        
        [Test]
        public void TestConfigManager_GetNamespace()
        {
            Assert.IsNotNull(_configManager);
            var ns1 = _configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests1");
            var ns2 = _configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests2");

            Assert.IsNotNull(ns1);
            Assert.IsNotNull(ns2);

            Assert.IsTrue(ns1.Name.Equals("Simple.Config.ConfigurationManager.FunctionalTests1"));
            Assert.IsTrue(ns2.Name.Equals("Simple.Config.ConfigurationManager.FunctionalTests2"));
        }
        
        [Test]
        public void TestConfigManager_GetProperty()
        {
            Assert.IsNotNull(_configManager);

            var p1 = _configManager.GetProperty("Simple.Config.ConfigurationManager.FunctionalTests1", "p1");
            Assert.IsNotNull(p1);

            Assert.IsTrue(p1.Name.Equals("p1"));
            Assert.IsTrue(p1.Value.Equals("v1"));
        }
        
        [Test]
        public void TestNamespace_Properties()
        {
            Assert.IsNotNull(_configManager);
            var namespaceObject = _configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests1");
            Assert.IsNotNull(namespaceObject);

            var pp = namespaceObject.Properties;
            Assert.IsNotNull(pp);
            Assert.IsTrue(pp.Length == 1);
            Assert.IsNotNull(pp[0]);
            
            Assert.IsTrue(pp[0].Value.Equals("v1"));
        }
        
        [Test]
        public void TestNamespace_GetProperty()
        {
            Assert.IsNotNull(_configManager);
            var namespaceObject = _configManager.GetNamespace("Simple.Config.ConfigurationManager.FunctionalTests1");
            Assert.IsNotNull(namespaceObject);
            var p = namespaceObject.GetProperty("p1");
            Assert.IsNotNull(p);
            
            Assert.IsTrue(p.Value.Equals("v1"));
        }
        
        [Test]
        public void TestProperty_NameAndValue()
        {
            Assert.IsNotNull(_configManager);
            var p1 = _configManager.GetProperty("Simple.Config.ConfigurationManager.FunctionalTests1", "p1");
            Assert.IsNotNull(p1);

            Assert.IsTrue(p1.Name.Equals("p1"));
            Assert.IsTrue(p1.Value.Equals("v1"));
            
            Assert.IsTrue(p1.Values.Count == 2);
            Assert.IsTrue(p1.Values[0].Equals("v1"));
        }
    }
}
