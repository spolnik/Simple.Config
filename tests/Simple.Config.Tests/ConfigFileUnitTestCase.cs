using NUnit.Framework;

namespace Simple.Config.Tests
{
    public class ConfigFileUnitTestCase
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
        public void Test_Namespaces()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.AreEqual(2, cf.Namespaces.Count);
            Assert.AreEqual("section1", cf.Namespaces[0].Name);

            _configManager.RemoveFile(@"test_files\test.ini");

            cf = _configManager.Load(@"test_files\test3.xml");
            
            Assert.IsNotNull(cf);
            Assert.AreEqual(1, cf.Namespaces.Count);
            Assert.AreEqual("Simple.Config.ConfigurationManager.FunctionalTests3", cf.Namespaces[0].Name);

            _configManager.RemoveFile(@"test_files\test3.xml");
        }
    }
}
