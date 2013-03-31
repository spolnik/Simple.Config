using NUnit.Framework;

namespace Simple.Config.Tests.Tutorial
{
    public class Simple_Usage
    {
        [Test]
        public void Load_and_check_values__xml()
        {
            var configManager = ConfigManager.GetInstance();
            var configFile = configManager.Load(@"http://ndatabase.net/test/AccuracyTest.xml");
            var property = configFile.Namespaces[0].GetProperty("AccuracyProp1");

            Assert.That(property.Value, Is.EqualTo("AccuracyValue4"));
        }
    }
}