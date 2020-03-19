using NUnit.Framework;
using FileManagerWithProfiles;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Util Tests

        [Test]
        public void TestLoginTrue()
        {
            Assert.IsTrue(Util.checkPassOrLogin("admin"));
        }

        [Test]
        public void TestLoginFalse()
        {
            Assert.IsFalse(Util.checkPassOrLogin("__admin"));
        }

        #endregion

        #region Auth Tests

        [Test]
        public void AutentificationForm()
        {
            Assert.DoesNotThrow(() => new AutentificationForm());
        }
        
        #endregion    

        #region Main Tests

        [Test]
        public void MainForm()
        {
            Assert.DoesNotThrow(() => new MainForm());
        }
        
        #endregion    

        #region Settings Tests

        [Test]
        public void SettingsForm()
        {
            Assert.DoesNotThrow(() => new SettingForm());
        }
        
        #endregion    
    }
}