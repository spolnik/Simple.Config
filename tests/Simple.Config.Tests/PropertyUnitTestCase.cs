using NUnit.Framework;

namespace Simple.Config.Tests
{
    public class PropertyUnitTestCase
    {
        private ConfigManager _configManager;
        
        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [TearDown]
        public void TearDown()
        {
			_configManager.Clear();
        }

        [Test]
        public void Test_Name()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            var namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);

            var prop = namespaceObject.GetProperty("prop1");
            Assert.IsNotNull(prop);
            Assert.AreEqual("prop1", prop.Name);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_Value()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            var namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);

            var prop = namespaceObject.GetProperty("prop1");
            Assert.IsNotNull(prop);
            Assert.AreEqual("value1", prop.Value);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_Values()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            var namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);

            var prop = namespaceObject.GetProperty("prop3");
            Assert.IsNotNull(prop);
            Assert.AreEqual("value1", prop.Value);
            Assert.AreEqual(3, prop.Values.Count);
            Assert.AreEqual("value3", prop.Values[2]);

            _configManager.RemoveFile(@"test_files\test.ini");
        }
    }
}
