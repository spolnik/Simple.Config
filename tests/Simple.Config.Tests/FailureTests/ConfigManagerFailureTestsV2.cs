using System;
using NUnit.Framework;

namespace Simple.Config.Tests.FailureTests
{
    public class ConfigManagerV2FailureTests
    {
        private const string File = @"test_files/failure_v2.xml";
        
        private const string Namespace = "NConfig.FailureTests.ConfigManagerV2FailureTests";

        private const string Property = "prop_name";

        private readonly ConfigManager _configManager = ConfigManager.GetInstance();

        [TestFixtureSetUp]
        public void LoadConfigurationFile()
        {
            _configManager.Load(File);
        }

        [TestFixtureTearDown]
        public void UnloadConfigurationFile()
        {
            _configManager.RemoveFile(File);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_NullNamespace()
        {
            _configManager.GetValue(null, Property);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_NullProperty()
        {
            _configManager.GetValue(Namespace, null);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_NullType()
        {
            _configManager.GetValue(Namespace, Property);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetValue_EmptyNamespace()
        {
            _configManager.GetValue(string.Empty, Property);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetValue_EmptyProperty()
        {
            _configManager.GetValue(Namespace, string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetValues_NullNamespace()
        {
            _configManager.GetValues(null, Property);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetValues_NullProperty()
        {
            _configManager.GetValues(Namespace, null);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void GetValues_NullType()
        {
            _configManager.GetValues(Namespace, Property);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetValues_EmptyNamespace()
        {
            _configManager.GetValues(string.Empty, Property);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetValues_EmptyProperty()
        {
            _configManager.GetValues(Namespace, string.Empty);
        }
    }

}
