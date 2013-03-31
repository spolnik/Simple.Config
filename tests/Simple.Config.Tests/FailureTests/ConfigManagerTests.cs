using NUnit.Framework;
using Simple.Config.Errors;

namespace Simple.Config.Tests.FailureTests
{
    public class ConfigManagerTests
    {
        private ConfigManager _configManager;
        private const string TestInvalidFilename = "not_here.ini";

        [SetUp]
        protected void SetUp()
        {
            _configManager = ConfigManager.GetInstance();
            _configManager.Load("test_files/failure_tests.xml");
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void LoadFileString_Null()
        {
            _configManager.Load(null);
        }

        [Test]
        public void LoadFileString_InvalidFilename()
        {
			try
			{
				_configManager.Load(TestInvalidFilename);
				Assert.Fail();
			}
			catch (System.IO.IOException)
			{
			}
			catch
			{
				Assert.Fail();
			}
        }

        [Test]
		[ExpectedException(typeof(PropertyClashException))]
		public void LoadFileString_PropertyClashException()
        {
			_configManager.Load("test_files/failure_tests2.xml");
		}

        [Test]
		[ExpectedException(typeof(InvalidConfigFileException))]
		public void LoadFileString_InvalidConfigFileException()
        {
			_configManager.Load("test_files/failure_tests3.xml");
		}

        [Test]
		[ExpectedException(typeof(InvalidConfigFileException))]
		public void LoadFileString_InvalidIniConfigFileException()
        {
			_configManager.Load("test_files/failure_tests3.ini");
		}

        [Test]
		[ExpectedException(typeof(UnknownFormatException))]
		public void LoadFileString_UnknownFormatException()
        {
			_configManager.Load("test_files/failure_tests4.tmp");
		}

        [TearDown]
        protected void TearDown()
        {
			_configManager.Clear();
		}
    }
}
