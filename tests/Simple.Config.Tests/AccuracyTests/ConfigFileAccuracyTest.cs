using NUnit.Framework;
using Simple.Config.Domain;

namespace Simple.Config.Tests.AccuracyTests
{
    public class ConfigFileAccuracyTest
    {
        private ConfigManager _configManager;
        private IConfigFile _configFile;

        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [Test]
        public void Test_GetNamespaces()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");
            Assert.AreEqual(_configFile.Namespaces.Count, 2);
            Assert.AreEqual(_configFile.Namespaces[0].Name, "AccuracyNamespace1");
            Assert.AreEqual(_configFile.Namespaces[1].Name, "AccuracyNamespace2");
            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");

            _configFile = _configManager.Load(@"test_files\AccuracyTest.xml");
            Assert.AreEqual(_configFile.Namespaces.Count, 2);
            Assert.AreEqual(_configFile.Namespaces[0].Name, "AccuracyNamespace3");
            Assert.AreEqual(_configFile.Namespaces[1].Name, "AccuracyNamespace4");
            _configManager.RemoveFile(@"test_files\AccuracyTest.xml");
        }
    }
}