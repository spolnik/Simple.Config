using NUnit.Framework;
using Simple.Config.Domain;

namespace Simple.Config.Tests.AccuracyTests
{
    public class NamespaceAccuracyTest
    {
        private ConfigManager _configManager;

        private readonly Namespace[] _namespaces = new Namespace[2];
        
        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
            _configManager.Load(@"test_files\AccuracyTest.ini");

            _namespaces[0] = _configManager.GetNamespace("AccuracyNamespace1");
            _namespaces[1] = _configManager.GetNamespace("AccuracyNamespace2");
        }

        [TearDown]
        public void TearDown()
        {
            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_GetName()
        {
            Assert.AreEqual(_namespaces[0].Name, "AccuracyNamespace1");
            Assert.AreEqual(_namespaces[1].Name, "AccuracyNamespace2");
        }

        [Test]
        public void Test_Properties()
        {
            Assert.AreEqual(_namespaces[0].Properties.Length, 3);
            Assert.AreEqual(_namespaces[0].Properties[0].Name, "AccuracyProp1");
            Assert.AreEqual(_namespaces[0].Properties[1].Name, "AccuracyProp2");
            Assert.AreEqual(_namespaces[0].Properties[2].Name, "AccuracyProp3");
            
            Assert.AreEqual(_namespaces[1].Properties.Length, 3);
            Assert.AreEqual(_namespaces[1].Properties[0].Name, "AccuracyProp1");
            Assert.AreEqual(_namespaces[1].Properties[1].Name, "AccuracyProp2");
            Assert.AreEqual(_namespaces[1].Properties[2].Name, "AccuracyProp3");
        }

        [Test]
        public void Test_GetProperty()
        {
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp1").Values.Count, 1);
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp1").Values[0], "AccuracyValue1");
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp2").Values.Count, 2);
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp2").Values[0], "AccuracyValue2");
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp2").Values[1], "AccuracyValue1");
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp3").Values.Count, 3);
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp3").Values[0], "AccuracyValue3");
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp3").Values[1], "AccuracyValue2");
            Assert.AreEqual(_namespaces[0].GetProperty("AccuracyProp3").Values[2], "AccuracyValue1");
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp1").Values.Count, 3);
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp1").Values[0], "AccuracyValue3");
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp1").Values[1], "AccuracyValue2");
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp1").Values[2], "AccuracyValue1");
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp2").Values.Count, 2);
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp2").Values[0], "AccuracyValue2");
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp2").Values[1], "AccuracyValue1");
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp3").Values.Count, 1);
            Assert.AreEqual(_namespaces[1].GetProperty("AccuracyProp3").Values[0], "AccuracyValue1");
        }

        [Test]
        public void Test_Indexer()
        {
            Assert.AreEqual(_namespaces[0]["AccuracyProp1"], "AccuracyValue1");
            Assert.AreEqual(_namespaces[0]["AccuracyProp2"], "AccuracyValue2");
            Assert.AreEqual(_namespaces[0]["AccuracyProp3"], "AccuracyValue3");
            Assert.AreEqual(_namespaces[1]["AccuracyProp1"], "AccuracyValue3");
            Assert.AreEqual(_namespaces[1]["AccuracyProp2"], "AccuracyValue2");
            Assert.AreEqual(_namespaces[1]["AccuracyProp3"], "AccuracyValue1");
        }
    }
}