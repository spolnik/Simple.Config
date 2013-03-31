using NUnit.Framework;
using Simple.Config.Domain;

namespace Simple.Config.Tests.AccuracyTests
{
    public class PropertyAccuracyTest
    {
        private ConfigManager _configManager;
        private readonly Property[] _properties = new Property[6];
        
        [SetUp]
        public void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
            _configManager.Load(@"test_files\AccuracyTest.ini");

            _properties[0] = _configManager.GetProperty("AccuracyNamespace1", "AccuracyProp1");
            _properties[1] = _configManager.GetProperty("AccuracyNamespace1", "AccuracyProp2");
            _properties[2] = _configManager.GetProperty("AccuracyNamespace1", "AccuracyProp3");
            _properties[3] = _configManager.GetProperty("AccuracyNamespace2", "AccuracyProp1");
            _properties[4] = _configManager.GetProperty("AccuracyNamespace2", "AccuracyProp2");
            _properties[5] = _configManager.GetProperty("AccuracyNamespace2", "AccuracyProp3");
        }

        [TearDown]
        public void TearDown()
        {
            _configManager.RemoveFile(@"test_files\AccuracyTest.ini");
        }

        [Test]
        public void Test_GetName()
        {
            Assert.AreEqual(_properties[0].Name, "AccuracyProp1");
            Assert.AreEqual(_properties[1].Name, "AccuracyProp2");
            Assert.AreEqual(_properties[2].Name, "AccuracyProp3");
            Assert.AreEqual(_properties[3].Name, "AccuracyProp1");
            Assert.AreEqual(_properties[4].Name, "AccuracyProp2");
            Assert.AreEqual(_properties[5].Name, "AccuracyProp3");
        }

        [Test]
        public void Test_GetValue()
        {
            Assert.AreEqual(_properties[0].Value, "AccuracyValue1");
            Assert.AreEqual(_properties[1].Value, "AccuracyValue2");
            Assert.AreEqual(_properties[2].Value, "AccuracyValue3");
            Assert.AreEqual(_properties[3].Value, "AccuracyValue3");
            Assert.AreEqual(_properties[4].Value, "AccuracyValue2");
            Assert.AreEqual(_properties[5].Value, "AccuracyValue1");
        }

        [Test]
        public void Test_GetValues()
        {
            Assert.AreEqual(_properties[0].Values.Count, 1);
            Assert.AreEqual(_properties[0].Values[0], "AccuracyValue1");
            Assert.AreEqual(_properties[1].Values.Count, 2);
            Assert.AreEqual(_properties[1].Values[0], "AccuracyValue2");
            Assert.AreEqual(_properties[1].Values[1], "AccuracyValue1");
            Assert.AreEqual(_properties[2].Values.Count, 3);
            Assert.AreEqual(_properties[2].Values[0], "AccuracyValue3");
            Assert.AreEqual(_properties[2].Values[1], "AccuracyValue2");
            Assert.AreEqual(_properties[2].Values[2], "AccuracyValue1");
            Assert.AreEqual(_properties[3].Values.Count, 3);
            Assert.AreEqual(_properties[3].Values[0], "AccuracyValue3");
            Assert.AreEqual(_properties[3].Values[1], "AccuracyValue2");
            Assert.AreEqual(_properties[3].Values[2], "AccuracyValue1");
            Assert.AreEqual(_properties[4].Values.Count, 2);
            Assert.AreEqual(_properties[4].Values[0], "AccuracyValue2");
            Assert.AreEqual(_properties[4].Values[1], "AccuracyValue1");
            Assert.AreEqual(_properties[5].Values.Count, 1);
            Assert.AreEqual(_properties[5].Values[0], "AccuracyValue1");
        }
    }
}