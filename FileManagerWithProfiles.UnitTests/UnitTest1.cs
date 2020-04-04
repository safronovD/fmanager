using FileManagerWithProfiles;
using NUnit.Framework;

namespace Tests
{
    public class UnitTests
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

        [Test]
        public void HashSize()
        {
            string pass = "password";
            string hashed = Util.PasswordHandler.CreatePasswordHash(pass);

            Assert.Equals(hashed.Length, 256);
        }

        #endregion
    }
}