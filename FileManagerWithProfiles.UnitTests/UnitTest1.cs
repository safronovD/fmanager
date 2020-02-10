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
    }
}