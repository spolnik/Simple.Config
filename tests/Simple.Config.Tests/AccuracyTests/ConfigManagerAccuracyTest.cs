using NUnit.Framework;
using Simple.Config.Domain;

namespace Simple.Config.Tests.AccuracyTests
{
    public class ConfigManagerAccuracyTest
    {
        private ConfigManager _configManager;
        private IConfigFile _configFile;

        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
        }

        [Test]
        public void Test_GetInstance()
        {
            Assert.IsNotNull(_configManager);
        }

        [Test]
        public void Test_LoadFile()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");
            Assert.AreEqual(_configFile.Namespaces.Count, 2);
            Assert.AreEqual(_configFile.Namespaces[0].Name, "AccuracyNamespace1");
            Assert.AreEqual(_configFile.Namespaces[1].Name, "AccuracyNamespace2");
            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_GetNamespace()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");

            Assert.AreEqual(_configManager.GetNamespace("AccuracyNamespace1").Name, "AccuracyNamespace1");
            Assert.AreEqual(_configManager.GetNamespace("AccuracyNamespace1").Properties.Length, 3);
            Assert.AreEqual(_configManager.GetNamespace("AccuracyNamespace2").Name, "AccuracyNamespace2");
            Assert.AreEqual(_configManager.GetNamespace("AccuracyNamespace2").Properties.Length, 3);

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_GetProperty()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");

            Assert.AreEqual(_configManager.GetProperty("AccuracyNamespace1", "AccuracyProp1").Value, "AccuracyValue1");
            Assert.AreEqual(_configManager.GetProperty("AccuracyNamespace1", "AccuracyProp2").Value, "AccuracyValue2");
            Assert.AreEqual(_configManager.GetProperty("AccuracyNamespace1", "AccuracyProp3").Value, "AccuracyValue3");
            Assert.AreEqual(_configManager.GetProperty("AccuracyNamespace2", "AccuracyProp1").Value, "AccuracyValue3");
            Assert.AreEqual(_configManager.GetProperty("AccuracyNamespace2", "AccuracyProp2").Value, "AccuracyValue2");
            Assert.AreEqual(_configManager.GetProperty("AccuracyNamespace2", "AccuracyProp3").Value, "AccuracyValue1");

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_Value()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");

            Assert.AreEqual(_configManager.GetValue("AccuracyNamespace1", "AccuracyProp1"), "AccuracyValue1");
            Assert.AreEqual(_configManager.GetValue("AccuracyNamespace1", "AccuracyProp2"), "AccuracyValue2");
            Assert.AreEqual(_configManager.GetValue("AccuracyNamespace1", "AccuracyProp3"), "AccuracyValue3");
            Assert.AreEqual(_configManager.GetValue("AccuracyNamespace2", "AccuracyProp1"), "AccuracyValue3");
            Assert.AreEqual(_configManager.GetValue("AccuracyNamespace2", "AccuracyProp2"), "AccuracyValue2");
            Assert.AreEqual(_configManager.GetValue("AccuracyNamespace2", "AccuracyProp3"), "AccuracyValue1");

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_Values()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");

            Assert.AreEqual(_configManager.GetValues("AccuracyNamespace1", "AccuracyProp1").Count, 1);
            Assert.AreEqual(_configManager.GetValues("AccuracyNamespace1", "AccuracyProp2").Count, 2);
            Assert.AreEqual(_configManager.GetValues("AccuracyNamespace1", "AccuracyProp3").Count, 3);
            Assert.AreEqual(_configManager.GetValues("AccuracyNamespace2", "AccuracyProp1").Count, 3);
            Assert.AreEqual(_configManager.GetValues("AccuracyNamespace2", "AccuracyProp2").Count, 2);
            Assert.AreEqual(_configManager.GetValues("AccuracyNamespace2", "AccuracyProp3").Count, 1);

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_GetConfigFile()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");
            _configFile = _configManager.Load(@"test_files\AccuracyTest.xml");

            Assert.AreEqual(_configManager.GetConfigFile(@"test_files\AccuracyTest.ini").Namespaces.Count, 2);
            Assert.AreEqual(_configManager.GetConfigFile(@"test_files\AccuracyTest.xml").Namespaces.Count, 2);

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
            _configManager.RemoveFile(@"test_files\AccuracyTest.xml");
        }

        [Test]
        public void Test_Indexer()
        {
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");
            Assert.AreEqual(_configManager["AccuracyNamespace1"].Name, "AccuracyNamespace1");
            Assert.AreEqual(_configManager["AccuracyNamespace1"].Properties.Length, 3);
            Assert.AreEqual(_configManager["AccuracyNamespace2"].Name, "AccuracyNamespace2");
            Assert.AreEqual(_configManager["AccuracyNamespace2"].Properties.Length, 3);
            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_GetNamespaces()
        {
            var len = _configManager.Namespaces.Length;
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");
            Assert.AreEqual(_configManager.Namespaces.Length - len, 2);
            _configFile = _configManager.Load(@"test_files\AccuracyTest.xml");
            Assert.AreEqual(_configManager.Namespaces.Length - len, 4);

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
            _configManager.RemoveFile(@"test_files\AccuracyTest.xml");
        }

        [Test]
        public void Test_GetConfigFiles()
        {
            var len = _configManager.ConfigFiles.Length;
            _configFile = _configManager.Load(@"test_files\AccuracyTest.ini");
            Assert.AreEqual(_configManager.ConfigFiles.Length - len, 1);
            _configFile = _configManager.Load(@"test_files\AccuracyTest.xml");
            Assert.AreEqual(_configManager.ConfigFiles.Length - len, 2);

            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
            _configManager.RemoveFile(@"test_files\AccuracyTest.xml");
        }
    }
}