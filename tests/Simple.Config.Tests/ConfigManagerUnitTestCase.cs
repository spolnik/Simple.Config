using NUnit.Framework;

namespace Simple.Config.Tests
{
    public class ConfigManagerUnitTestCase
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
            Assert.AreEqual(4, _configManager.Namespaces.Length);

            _configManager.RemoveFile(@"test_files\test.ini");

            cf = _configManager.Load(@"test_files\test3.xml");
            
            Assert.IsNotNull(cf);
            Assert.AreEqual(3, _configManager.Namespaces.Length);

            _configManager.RemoveFile(@"test_files\test3.xml");
        }

        [Test]
        public void Test_ConfigFiles()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.AreEqual(3, _configManager.ConfigFiles.Length);

            _configManager.RemoveFile(@"test_files\test.ini");

            Assert.AreEqual(2, _configManager.ConfigFiles.Length);

            cf = _configManager.Load(@"test_files\test3.xml");
            
            Assert.IsNotNull(cf);
            Assert.AreEqual(3, _configManager.ConfigFiles.Length);

            _configManager.RemoveFile(@"test_files\test3.xml");

            Assert.AreEqual(2, _configManager.ConfigFiles.Length);
        }

        [Test]
        public void Test_GetConfigFile()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.AreEqual(cf.Namespaces.Count, _configManager.GetConfigFile(@"test_files\test.ini").Namespaces.Count);

            _configManager.RemoveFile(@"test_files\test.ini");

            Assert.AreEqual(1, _configManager.GetConfigFile(@"test_files\test1.xml").Namespaces.Count);

        }

        [Test]
        public void Test_GetNamespace()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager.GetNamespace("section1"));

            Assert.AreEqual("section1", _configManager.GetNamespace("section1").Name);
            Assert.AreEqual(3, _configManager.GetNamespace("section1").Properties.Length);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_Indexer()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager["section1"]);
            Assert.AreEqual("section1", _configManager["section1"].Name);
            Assert.AreEqual(3, _configManager["section1"].Properties.Length);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_GetProperty()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager.GetProperty("section1", "prop1"));
            Assert.AreEqual("prop1", _configManager.GetProperty("section1", "prop1").Name);
            Assert.AreEqual(1, _configManager.GetProperty("section1", "prop1").Values.Count);
            Assert.AreEqual(3, _configManager.GetProperty("section1", "prop3").Values.Count);
            Assert.AreEqual(2, _configManager.GetProperty("section2", "prop2").Values.Count);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_GetValue()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager.GetValue("section1", "prop1"));
            Assert.AreEqual("value1", _configManager.GetValue("section1", "prop1"));
            Assert.AreEqual("value1", _configManager.GetValue("section1", "prop3"));
            Assert.AreEqual("value1", _configManager.GetValue("section2", "prop2"));
            Assert.AreEqual("value1", _configManager.GetValue("section2", "prop3"));

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_GetValues()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager.GetValues("section1", "prop1"));
            Assert.AreEqual(1, _configManager.GetValues("section1", "prop1").Count);
            Assert.AreEqual(3, _configManager.GetValues("section1", "prop3").Count);
            Assert.AreEqual(2, _configManager.GetValues("section2", "prop2").Count);
            Assert.AreEqual(3, _configManager.GetValues("section2", "prop3").Count);
            Assert.AreEqual("value2", _configManager.GetValues("section2", "prop2")[1]);
            Assert.AreEqual("value3", _configManager.GetValues("section1", "prop3")[2]);
            Assert.AreEqual("value1", _configManager.GetValues("section2", "prop3")[0]);

            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_GetValue2()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager.GetValue("section1", "prop1"));
            Assert.AreEqual("value1", _configManager.GetValue("section1", "prop3"));
            Assert.AreEqual(3, _configManager.GetValues("section1", "prop3").Count);
            
            _configManager.RemoveFile(@"test_files\test.ini");
        }

        [Test]
        public void Test_GetValues2()
        {
            Assert.IsNotNull(_configManager);
            var cf = _configManager.Load(@"test_files\test.ini");

            Assert.IsNotNull(cf);
            Assert.IsNotNull(_configManager.GetValue("section1", "prop1"));
            Assert.AreEqual("value1", _configManager.GetValue("section1", "prop3"));
            Assert.AreEqual(3, _configManager.GetValues("section1", "prop3").Count);
            
            _configManager.RemoveFile(@"test_files\test.ini");
        }
    }
}
