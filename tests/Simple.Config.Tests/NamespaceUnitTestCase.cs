using NUnit.Framework;
using Simple.Config.Domain;
using Simple.Config.Errors;

namespace Simple.Config.Tests
{
    public class NamespaceUnitTestCase
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
        public void Test_GetProperty()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            var namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);

            var prop = namespaceObject.GetProperty("prop3");
            Assert.IsNotNull(prop);
            Assert.AreEqual(3, prop.Values.Count);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_Properties()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            Namespace namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);
            Assert.AreEqual(3, namespaceObject.Properties.Length);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_Indexer()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            var namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);

            var propValue = namespaceObject["prop3"];
            Assert.AreEqual("value1", propValue);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_Name()
        {
            Assert.IsNotNull(_configManager);
            _configManager.Load(@"test_files\test.ini");
            
            var namespaceObject = _configManager.GetNamespace("section2");
            Assert.IsNotNull(namespaceObject);
            Assert.AreEqual("section2", namespaceObject.Name);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test, ExpectedException(typeof(NamespaceClashException))]
        public void Test_DoubleLoadOfTheFileWithTheSameValues()
        {
            _configManager.Load(@"test_files\test.ini");
            _configManager.Load(@"test_files\test_copy.ini");
        }

    }
}
